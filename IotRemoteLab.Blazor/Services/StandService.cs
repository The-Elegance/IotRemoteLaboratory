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
        public Guid EditorElementId { get; }


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
        public string DebugUpload { get; set; } = string.Empty;


        #region Constructors


        public StandService(HttpClient httpClient, HubConnection hubConnection, Guid id, CancellationToken cancellationToken = default)
        {
            _httpClient = httpClient;
            _hubConnection = hubConnection;
            _id = id;

            //Init(id, cancellationToken);            
        }

        public async Task Init(CancellationToken cancellationToken = default)
        {
            _stand = await _httpClient.GetFromJsonAsync<Stand>($"api/stands/{_id}", cancellationToken);
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
            _hubConnection.On<Guid, string>("TerminalLogAdded", OnTerminalLogAdded);
            _hubConnection.On<string>("DebugUploadChanged", OnDebugUploadChanged);
            await _hubConnection.SendAsync("EnterToStand", _id);
            //_hubConnection.On<Guid, Guid, string>("CodeExecuteResultChanged", OnCodeExecuteResultChanged);
        }

        private void OnDebugUploadChanged(string arg2)
        {
            DebugUpload = arg2;
            Console.WriteLine(arg2);
            StandStateChanged?.Invoke();
        }

        private void OnTerminalLogAdded(Guid guid, string log)
        {
            TerminalLogs.Add(log);
            Console.WriteLine(log);
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
            ConsoleDebug.WriteLine($"LastSelectedUart {guid}");
            SelectedUart = AvailableUarts.FirstOrDefault(x => x.Id == guid);
            foreach (var au in AvailableUarts)
            {
                ConsoleDebug.WriteLine(guid);
            }
            StandStateChanged?.Invoke();
        }


        #endregion Constructors


        #region Public Methods


        private void OnCodeExecuteResultChanged(Guid standId, Guid elementId, string result)
        {

        }

        private void OnRaspberryPiInPortChanged(Guid guid, byte arg2)
        {
            StandLeds.FirstOrDefault(s => s.PortType == PortType.RaspberryPi).IsEnable = arg2 > 0;
            ConsoleDebug.WriteLine(arg2);
            StandStateChanged?.Invoke();
        }


        /**
         *
         * Методы SignalR
         * 
         * SendToTopic(topic, args) - Отправляет об изменения по топику данные на сервер, откуда они уже передаются по Mqtt к стенду.
         * TopicValueChanged<topic, args> - Событие которые вызывается, когда кто-то вызывает метод SendToTopic, возвращает topic и args.
         * 
         */


        /// <summary>
        /// Отправляет команду отправленную из терминала, на сервер через SignalR.
        /// </summary>
        /// <param name="command">Команда терминала.</param>
        public void TerminalSendCommand(string command)
        {
            if (command != null && command?.Length == 0)
                return;

            _hubConnection.SendAsync("SendToTopic", "", command);
        }
        // может MonacoEditor id хранить в базе данных, чтобы оно было уникальное для каждого стенда?


        #endregion Public Methods


        #region Private Methods


        /// <summary>
        /// Данные топика были изменены на сервере, автор SignalR.
        /// </summary>
        private void OnTopicValueChanged(string topic, string args)
        {
            StandStateChanged?.Invoke();
        }


        #endregion Private Methods
    }
}
