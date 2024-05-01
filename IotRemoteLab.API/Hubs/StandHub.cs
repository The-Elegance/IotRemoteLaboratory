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
         * Клиентские методы - предназначены для вызова клиентом.
         * 
         * **/

        public async Task TerminalCommandSend(Guid standId, DateTime time, Guid sessionId, string command) 
        {
            await Clients.Group(standId.ToString()).SendAsync("OnTerminalCommandAdded", standId, time, sessionId, command);
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

        /// <summary>
        /// Raspberry Pi работает в режиме отслеживания состояния контакта (подаёт сигнал 0 или 1 на контакт)
        /// </summary>
        /// <returns></returns>
        public async Task RaspberryPiInPort(Guid standId, Guid guid, int subport, bool signalValue)
        {
            await Clients.All.SendAsync("RaspberryPiInPortChanged", guid, subport, signalValue);
        }
    }
}
