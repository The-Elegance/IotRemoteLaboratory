using IotRemoteLab.Blazor.Models;
using IotRemoteLab.Blazor.Tools;
using IotRemoteLab.Domain.Code;
using IotRemoteLab.Domain.Stand;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Json;

namespace IotRemoteLab.Blazor.Services
{
    public class StandService
    {
        public event Action StandDataLoaded;
        public event Action LedStateChanged;
        public event Action StandStateChanged;
        public event Action TerminalLogsChanged;
        public event Action<string> EnterDeltaDataDelivered;

        private readonly HttpClient _httpClient;
        private readonly HubConnection _hubConnection;

        /// <summary>
        /// Guid стенда
        /// </summary>
        private readonly Guid _id;
        /// <summary>
        /// Экземпляр класса стенд.
        /// </summary>
        private Stand? _stand;
        /// <summary>
        /// Id контейнера ide для стенда. Чтобы не допустить изменение значений из других страниц стендов.
        /// </summary>
        public Guid EditorElementId { get; set; }


        private Guid _lastSelectedUart;


        /// <summary>
        /// Список доступных стенду uart.
        /// </summary>
        public List<Uart> AvailableUarts { get => _stand?.AvailableUarts ?? []; }
        /// <summary>
        /// Выбранный uart.
        /// </summary>
        private Uart _selectedUart;
        public Uart SelectedUart // 4 | 3
        {
            get => _selectedUart; set
            {
                _selectedUart = value;
                if (_lastSelectedUart == value.Id)
                    return;

                _lastSelectedUart = value.Id;
                _hubConnection.SendAsync("SelectUart", value.Id);
            }
        }

        public List<StandLed> StandLeds { get; set; } = [];
        public List<StandButton> StandButton { get; set; } = [];
        public ExecutionCodeResult ExecutionCodeResult { get; }
        public List<string> TerminalLogs { get; set; } = [];
        public bool IsWebcameraEnable { get; set; }


        public BoilerplateCode DefaultBoilerplateCode { get; set; }
        public string DebugUpload { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;


        #region Constructors


        public StandService(HttpClient httpClient, HubConnection hubConnection, Guid id, CancellationToken cancellationToken = default)
        {
            _httpClient = httpClient;
            _hubConnection = hubConnection;
            _id = id;   
        }

        public async Task Init(CancellationToken cancellationToken = default)
        {
            _stand = await _httpClient.GetFromJsonAsync<Stand>($"api/stands/{_id}", cancellationToken);
            EditorElementId = _stand.CodeEditorId;
            DefaultBoilerplateCode = new BoilerplateCode("cpp", "14", _stand.Framework.Pattern);

            var selectedUart = AvailableUarts.FirstOrDefault();
            _lastSelectedUart = selectedUart.Id;
            SelectedUart = selectedUart;

            if (_stand != null)
            {
                foreach (var port in _stand.Benchboard.Ports)
                {
                    if (port.Type == Domain.Stand.Benchboards.BenchboardPortType.Output)
                    {
                        // mcu led
                        StandLeds.Add(new StandLed()
                        {
                            Id = port.Id,
                            PortType = PortType.Mcu,
                            Name = port.McuPort,
                            IsEnable = false
                        });

                        StandButton.Add(new StandButton()
                        {
                            Id = port.Id,
                            PortType = PortType.RaspberryPi,
                            Name = port.RaspberryPiPort.ToString(),
                            IsChecked = false
                        });

                        continue;
                    }

                    StandLeds.Add(new StandLed()
                    {
                        Id = port.Id,
                        PortType = PortType.RaspberryPi,
                        Name = port.RaspberryPiPort.ToString(),
                        IsEnable = false
                    });

                    StandButton.Add(new StandButton()
                    {
                        Id = port.Id,
                        Name = port.McuPort,
                        PortType = PortType.Mcu,
                        IsChecked = false
                    });
                }
            }

            _hubConnection.On<Guid>("UartTypeChanged", OnUartTypeChanged);
            _hubConnection.On<Guid, int, bool>("GpioLedStateChanged", OnGpioLedPortChanged);
            _hubConnection.On<Guid, string>("TerminalDataUpdatedFromServer", OnTerminalDataUpdatedFromServer);
            _hubConnection.On<string>("DebugUploadChanged", OnDebugUploadChanged);
            _hubConnection.On<bool>("WebcameraStateChanged", OnWebcameraStateChanged);
            _hubConnection.On<StandDeltaData>("DeltaDataDelivered", OnDeltaDataDelivered);

            _hubConnection.On<Guid, DateTime, Guid, string>("OnTerminalCommandAdded", OnTerminalCommandAdded);

            await _hubConnection.SendAsync("EnterToStand", _id);
            //_hubConnection.On<Guid, Guid, string>("CodeExecuteResultChanged", OnCodeExecuteResultChanged);
            ConsoleDebug.WriteLine("Init finished");
        }


        #endregion Constructors


        #region Public Methods


        private void OnCodeExecuteResultChanged(Guid standId, Guid elementId, string result)
        {

        }

        /// <summary>
        /// Отправляет команду отправленную из терминала, на сервер через SignalR.
        /// </summary>
        /// <param name="command">Команда терминала.</param>
        public void TerminalSendCommand(DateTime time, Guid sessionId, string command)
        {
            if (command != null && command?.Length == 0)
                return;

            _hubConnection.SendAsync("TerminalCommandSend", _id, time, sessionId, command);
        }
        // может MonacoEditor id хранить в базе данных, чтобы оно было уникальное для каждого стенда?


        public void ButtonStateChanged(Guid portId, PortType portType, bool state) 
        {

        }


        #endregion Public Methods


        #region Private Methods


        private void OnDeltaDataDelivered(StandDeltaData data)
        {
            ConsoleDebug.WriteLine(data.Code);//.Code);
            Code = data.Code;
            DebugUpload = data.DebugUploadOutput;
            StandStateChanged?.Invoke();
            EnterDeltaDataDelivered?.Invoke(data.Code);
        }

        private void OnWebcameraStateChanged(bool state)
        {
            IsWebcameraEnable = state;
            StandStateChanged?.Invoke();
        }

        private void OnDebugUploadChanged(string arg2)
        {
            DebugUpload = arg2;
            StandStateChanged?.Invoke();
        }

        private void OnTerminalDataUpdatedFromServer(Guid guid, string log)
        {
            TerminalLogs.Add(log);
            StandStateChanged?.Invoke();
            TerminalLogsChanged?.Invoke();
        }

        private void OnGpioLedPortChanged(Guid guid, int arg2, bool arg3)
        {
            StandLeds.FirstOrDefault(x => x.Id == guid).IsEnable = arg3;
            StandStateChanged?.Invoke();
        }

        private void OnUartTypeChanged(Guid guid)
        {
            _lastSelectedUart = guid;

            SelectedUart = AvailableUarts.FirstOrDefault(x => x.Id == guid);
            StandStateChanged?.Invoke();
        }

        private void OnTerminalCommandAdded(Guid standId, DateTime time, Guid sessionId, string command) 
        {
            TerminalLogs.Add($"[{time}] [{sessionId}] {command}");
            StandStateChanged?.Invoke();
            TerminalLogsChanged?.Invoke();
        }


        #endregion Private Methods
    }
}
