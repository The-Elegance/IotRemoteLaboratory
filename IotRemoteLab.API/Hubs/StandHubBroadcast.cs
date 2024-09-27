using IotRemoteLab.API.MqttTopicHandlers;
using IotRemoteLab.Mqtt;
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
            s.DebugUpload += S_DebugUpload;
            s.Execute();
        }

        private async void S_DebugUpload(long arg1, string arg2)
        {
            await SendDebugUpload(arg1, arg2);
        }

        private async void S_SerialIn(long arg1, string arg2)
        {
            await SendTerminalLog(arg1, arg2);
        }

        private async void S_GpioLed(long arg1, string arg3, bool arg4)
        {
            await SendNewGpioLedState(arg1, arg3, arg4);
        }

        public async Task SendNewGpioLedState(long standId, string port, bool signalValue)
        {
            await _standHub.Clients.Group(standId.ToString()).SendAsync("GpioLedStateChanged", port, signalValue);
        }

        public async Task SendDebugUpload(long standId, string value) 
        {
            await _standHub.Clients.Group(standId.ToString()).SendAsync("DebugUploadChanged", value);
        }

        public async Task SendCodeExecuteResult(long standId, Guid editorElementId, string result)
        {
            await _standHub.Clients.Group(standId.ToString()).SendAsync("CodeExecuteResultChanged", standId, editorElementId, result);
        }

        public async Task SendTerminalLog(long standId, string value)
        {
            await _standHub.Clients.Group(standId.ToString()).SendAsync("TerminalDataUpdatedFromServer", standId, value);
        }
    }
}
