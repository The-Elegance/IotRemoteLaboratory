using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;

namespace IotRemoteLab.Mqtt
{
    public sealed class MqttPublisher : MqttMember
    {
        public override IEnumerable<string> Topics { get; }
        protected override IMqttClient Client { get; }
        protected override IMqttClientOptions Options { get; }


        public MqttPublisher(MqttParams mqttParams)
        {
            Client = _mqttFactory.CreateMqttClient();
            Options = new MqttClientOptionsBuilder()
                .WithClientId(Guid.NewGuid().ToString())
                .WithTcpServer(mqttParams.Ip, mqttParams.Port)
                .WithCleanSession()
                .Build();

            Client.UseConnectedHandler(Connected);
            Client.UseDisconnectedHandler(Disconnected);
        }

        protected override async Task Connected(MqttClientConnectedEventArgs e)
        {
            Console.WriteLine("Connected to the broker successful");
        }

        protected override Task Disconnected(MqttClientDisconnectedEventArgs e)
        {
            Console.WriteLine("Disconnect from the broker successful");
            return Task.CompletedTask;
        }

        /// <summary>
        /// Получает строку либо json в виде строки
        /// </summary>
        /// <param name="payload"></param>
        public async void PublishMessageAsync(string topic, string payload)
        {
            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(payload)
                .WithAtMostOnceQoS()
                .WithRetainFlag(false)
                .Build();

            if (Client.IsConnected)
                await Client.PublishAsync(message);
        }
    }
}