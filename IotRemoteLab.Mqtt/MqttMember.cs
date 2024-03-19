using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Client;
using MQTTnet;

namespace IotRemoteLaboratory.Mqtt.Core
{
    public abstract class MqttMember
    {
        protected readonly MqttFactory _mqttFactory = new();
        public abstract IEnumerable<string> Topics { get; }
        protected abstract IMqttClientOptions Options { get; }
        protected abstract IMqttClient Client { get; }
        public bool IsConnected { get; private set; }


        public virtual async void Connect()
        {
            IsConnected = true;
            await Client.ConnectAsync(Options, CancellationToken.None);
        }

        public virtual async void Disconnect()
        {
            IsConnected = false;
            await Client.DisconnectAsync();
        }

        public async void SubscribeToTopic(string topic)
        {
            var filter = new MqttTopicFilterBuilder()
                .WithTopic(topic)
                .Build();

            await Client.SubscribeAsync(filter);
        }

        public async void UnsubscribeTopic(string topic)
        {
            await Client.UnsubscribeAsync(topic);
        }

        protected abstract Task Connected(MqttClientConnectedEventArgs e);
        protected abstract Task Disconnected(MqttClientDisconnectedEventArgs e);
    }
}