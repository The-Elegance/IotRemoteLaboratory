using IotRemoteLab.API.Services;
using IotRemoteLab.Domain.Stand;
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
            _standsService.ExecuteCommand(standId, command);
        }

        public async Task CodeUpdate(Guid standId, string newValue) 
        {
            _standsService.AddDeltaData(standId, newValue, null);
            await Clients.Group(standId.ToString()).SendAsync("OnCodeUpdated", newValue);
        }

        public async Task SelectUart(Guid standId, Uart uart) 
        {
            await Clients.Group(standId.ToString()).SendAsync("UartTypeChanged", uart.Id);
            _standsService.PublishMessageAsync(Topics.UartType.Replace("+", standId.ToString()), uart.Index.ToString());
        }

        public async Task ChangePortState(Guid standId, string port, bool state) 
        {
            await Clients.Group(standId.ToString()).SendAsync("OnPortStateChanged", port, state);
            _standsService.PublishMessageAsync(Topics.ButtonNoLedState.Replace("+", standId.ToString()).Replace("#", port), state ? "1" : "0");
        }
    }
}
