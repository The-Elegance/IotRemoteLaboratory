using IotRemoteLab.API.MqttTopicHandlers;
using IotRemoteLaboratory.Mqtt.Core;
using Microsoft.AspNetCore.SignalR;

namespace IotRemoteLab.API.Hubs
{
    public class StandHubBroadcast
    {
        private readonly IHubContext<StandHub> _standHub;
        private readonly MqttSubscriber _mqttSubscriber;

        public StandHubBroadcast(IHubContext<StandHub> standHub, MqttSubscriber mqttSubscriber) 
        {
            _standHub = standHub;
            _mqttSubscriber = mqttSubscriber;

            _mqttSubscriber.MessageReceivedEvent += _mqttSubscriber_MessageReceivedEvent;
        }

        private void _mqttSubscriber_MessageReceivedEvent(string topic, string value)
        {
            var s = new LabStandHandler(topic, value);
            s.GpioLed += S_GpioLed;
            s.SerialIn += S_SerialIn;
            s.Execute();
        }

        private async void S_SerialIn(Guid arg1, string arg2)
        {
            await SendTerminalLog(arg1, arg2);
        }

        private async void S_GpioLed(Guid arg1, Guid arg2, int arg3, bool arg4)
        {
            await SendNewGpioLedState(arg1, arg2, arg3, arg4);
        }

        public async Task SendNewGpioLedState(Guid standId, Guid guid, int subport, bool signalValue)
        {
            await _standHub.Clients.Group(standId.ToString()).SendAsync("GpioLedStateChanged", guid, subport, signalValue);
        }

        public async Task SendDebugUpload() 
        {
            
        }

        public async Task SendCodeExecuteResult(Guid standId, Guid editorElementId, string result)
        {
            await _standHub.Clients.Group(standId.ToString()).SendAsync("CodeExecuteResultChanged", standId, editorElementId, result);
        }

        public async Task SendTerminalLog(Guid standId, string value)
        {
            await _standHub.Clients.Group(standId.ToString()).SendAsync("TerminalLogAdded", standId, value);
        }

    }
}
