using Microsoft.AspNetCore.SignalR;
using System.Numerics;
using IotRemoteLab.Domain;
using IotRemoteLab.Domain.Stand;

namespace IotRemoteLab.API.Hubs
{
    public class StandHub : Hub
    {
        public async Task SendToTopic(string topic, string args) 
        {
            await Clients.All.SendAsync("StandStateChanged", topic, args);
        }
        public async Task AddFramework(McuFramework framework)
        {
            await Clients.All.SendAsync("FrameworkAdded", framework);
        }

        public async Task AddMcu(Mcu mcu)
        {
            await Clients.All.SendAsync("McuAdded", mcu);
        }

        public async Task AddPort(McuFramework newFramework)
        {
            await Clients.All.SendAsync("FrameworkAdded", newFramework);
        }

        public async Task AddBenchTable(McuFramework newFramework)
        {
            await Clients.All.SendAsync("FrameworkAdded", newFramework);
        }

        public async Task AddStand(McuFramework newFramework)
        {
            await Clients.All.SendAsync("FrameworkAdded", newFramework);
        }
    }
}
