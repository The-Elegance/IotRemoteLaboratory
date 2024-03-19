using Microsoft.AspNetCore.SignalR;

namespace IotRemoteLab.API.Hubs
{
    public class StandHub : Hub
    {
        public async Task SendToTopic(string topic, string args) 
        {
            await Clients.All.SendAsync("StandStateChanged", topic, args);
        }
    }
}
