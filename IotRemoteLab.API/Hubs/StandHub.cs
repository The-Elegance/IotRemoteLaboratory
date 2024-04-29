using IotRemoteLab.API.MqttTopicHandlers;
using IotRemoteLab.API.Services;
using IotRemoteLaboratory.Mqtt.Core;
using Microsoft.AspNetCore.SignalR;

namespace IotRemoteLab.API.Hubs
{
    public class StandHub : Hub
    {
        private readonly StandsService _standsService;

        public StandHub(StandsService standsService)
        {
            _standsService = standsService;
        }   
        
        public async Task EnterToStand(Guid standId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, standId.ToString());
            await Clients.Client(Context.ConnectionId).SendAsync("DeltaDataDelivered", _standsService.GetDeltaData(standId));
        }

        /*
         * Серверные методы - предназначены для вызова сервером.
         * SendCodeExecuteResult - отправляет результат запуска кода, всем пользователям в комнате.
         * SendTerminalLog - отправляет новый лог в терминал.
         * 
         * **/


        // /lab/stand/+/serial/in


        /// <summary>
        /// Raspberry Pi работает в режиме отслеживания состояния контакта (подаёт сигнал 0 или 1 на контакт)
        /// </summary>
        /// <returns></returns>
        public async Task RaspberryPiInPort(Guid standId, Guid guid, int subport, bool signalValue)
        {
            await Clients.All.SendAsync("RaspberryPiInPortChanged", guid, subport, signalValue);
        }


        /*
         * Клиентские методы - предназначены для вызова клиентом.
         * 
         * **/

        public override Task OnConnectedAsync()
        {

            Console.WriteLine(Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public async Task SendToTopic(string topic, string args) 
        {
            await Clients.All.SendAsync("TopicValueChanged", topic, args);
        }

        public async Task CodeUpdate(Guid standId, string newValue) 
        {
            _standsService.AddDeltaData(standId, newValue, null);
            await Clients.Group(standId.ToString()).SendAsync("OnCodeUpdated", newValue);
        }

        public async Task SelectUart(Guid id) 
        {
            await Clients.Others.SendAsync("UartTypeChanged", id);
        }

        /// <summary>
        /// Raspberry Pi эмулирует нажатие кнопки (подаёт сигнал 0 или 1 на контакт)
        /// </summary>
        /// <returns></returns>
        public async Task RaspberryPiOutPort(byte signalValue) 
        {
            await Clients.All.SendAsync("RaspberryPiOutPortChanged", signalValue);
        }
    }
}
