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
        public event Action? StandDataLoaded;
        public event Action? LedStateChanged;
        public event Action? StandStateChanged;
        public event Action? TerminalLogsChanged;
        public event Action<string>? EnterDeltaDataDelivered;

        private readonly HttpClient _httpClient;
        private readonly HubConnection _hubConnection;

        /// <summary>
        /// Guid стенда
        /// </summary>
        private readonly long _id;
        /// <summary>
        /// Экземпляр класса стенд.
        /// </summary>
        private Stand? _stand;
        /// <summary>
        /// Id контейнера ide для стенда. Чтобы не допустить изменение значений из других страниц стендов.
        /// </summary>
        public Guid EditorElementId { get; set; }


        private long _lastSelectedUart;

        /// <summary>
        /// Название стенда.
        /// </summary>
        public string McuName { get; private set; }
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
                _hubConnection.SendAsync("SelectUart", value);
            }
        }

        public List<StandLed> StandLeds { get; set; } = [];
        public HashSet<StandButton> StandButton { get; set; } = [];
        public ExecutionCodeResult ExecutionCodeResult { get; }
        public List<string> TerminalLogs { get; set; } = [];
        public bool IsWebcameraEnable { get; set; }


        public BoilerplateCode DefaultBoilerplateCode { get; set; }
        public string DebugUpload { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;


        #region Constructors


        public StandService(Stand stand, HttpClient httpClient, HubConnection hubConnection)
        {
            _httpClient = httpClient;
            _hubConnection = hubConnection;
            _id = stand.Id;
            _stand = stand;

            EditorElementId = _stand.CodeEditorId;
            DefaultBoilerplateCode = new BoilerplateCode("cpp", "14", _stand.Framework.Pattern);

            var selectedUart = stand.AvailableUarts.First();
            _lastSelectedUart = selectedUart.Id;
            SelectedUart = selectedUart;

            LoadBenchboardPorts(_stand);

            // SignalR Prepare
            SubscribeOnSignalRHubUnits();
        }


        #endregion Constructors


        #region Public Methods


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

        public async void ButtonStateChanged(string portId, bool state)
        {
            await _hubConnection.SendAsync("ChangePortState", _id, portId, state);
        }


        #endregion Public Methods


        #region Private Methods


        private void OnPortStateChanged(string port, bool state)
        {
            var foundPort = StandButton.FirstOrDefault(b => b.Name == port);
            if (foundPort != null)
                foundPort.IsChecked = state;
            StandStateChanged?.Invoke();
        }

        /// <summary>
        /// Подсписываемся на обновления данных через SignalR
        /// </summary>
        private void SubscribeOnSignalRHubUnits()
        {
            _hubConnection.On<long>("UartTypeChanged", OnUartTypeChanged);
            _hubConnection.On<string, bool>("GpioLedStateChanged", OnGpioLedPortChanged);
            _hubConnection.On<Guid, string>("TerminalDataUpdatedFromServer", OnTerminalDataUpdatedFromServer);
            _hubConnection.On<string>("DebugUploadChanged", OnDebugUploadChanged);
            _hubConnection.On<bool>("WebcameraStateChanged", OnWebcameraStateChanged);
            _hubConnection.On<StandDeltaData>("DeltaDataDelivered", OnDeltaDataDelivered);
            _hubConnection.On<string, bool>("OnPortStateChanged", OnPortStateChanged);
            _hubConnection.On<Guid, DateTime, Guid, string>("OnTerminalCommandAdded", OnTerminalCommandAdded);
        }

        /// <summary>
        /// Загружает порты в списки, которые выводятся на экран.
        /// </summary>
        /// <param name="stand">Данные стенда</param>
        private void LoadBenchboardPorts(Stand stand)
        {
            foreach (var port in stand.Benchboard.Ports)
            {
                if (port.Type == Domain.Stand.Benchboards.BenchboardPortType.Input)
                {
                    // mcu led
                    StandLeds.Add(new StandLed()
                    {
                        Id = port.Id,
                        PortType = PortType.Mcu,
                        Name = port.McuPort,
                        IsEnable = false
                    });

                    continue;
                }

                StandButton.Add(new StandButton()
                {
                    Id = port.Id,
                    Name = port.McuPort,
                    PortType = PortType.Mcu,
                    IsChecked = true
                });
            }
        }

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

        private void OnGpioLedPortChanged(string arg2, bool arg3)
        {
            var foundPort = StandLeds.FirstOrDefault(x => x.Name == arg2);

            if (foundPort == null)
                return;

            foundPort.IsEnable = arg3;
            StandStateChanged?.Invoke();
        }

        private void OnUartTypeChanged(long id)
        {
            _lastSelectedUart = id;

            SelectedUart = _stand.AvailableUarts.FirstOrDefault(x => x.Id == id);
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
