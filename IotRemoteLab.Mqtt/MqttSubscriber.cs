using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Client;
using MQTTnet;
using System.Text;

namespace IotRemoteLaboratory.Mqtt.Core
{
    public sealed class MqttSubscriber : MqttMember
    {
        /// <summary>
        /// Когда сообщение приходит с брокера возвращает json в виде строки или просто строку.
        /// Принимает Topic, Message.
        /// </summary>
        public event Action<string, string> MessageReceivedEvent;

        public override IEnumerable<string> Topics { get; }
        protected override IMqttClientOptions Options { get; }
        protected override IMqttClient Client { get; }

        public MqttSubscriber(MqttParams mqttParams)
        {

            Topics = mqttParams.Topics;
            Client = _mqttFactory.CreateMqttClient();
            Options = new MqttClientOptionsBuilder()
                .WithClientId(Guid.NewGuid().ToString())
                .WithTcpServer(mqttParams.Ip, mqttParams.Port)
                .WithCleanSession()
                .Build();

            Client.UseConnectedHandler(Connected);
            Client.UseDisconnectedHandler(Disconnected);
            Client.UseApplicationMessageReceivedHandler(MessageReceived);
        }

        protected override async Task Connected(MqttClientConnectedEventArgs e)
        {
            foreach (var topic in Topics)
            {
                SubscribeToTopic(topic);
            }
        }

        protected override Task Disconnected(MqttClientDisconnectedEventArgs e)
        {
            Console.WriteLine("Disconnect from the broker successful");
            return Task.CompletedTask;
        }

        /// <summary>
        /// Закидывает в эвент строку json 
        /// </summary>
        /// <param name="e"></param>
        private void MessageReceived(MqttApplicationMessageReceivedEventArgs e)
        {
            var msg = e.ApplicationMessage.Payload;

            msg = msg ?? Array.Empty<byte>();

            var value = Encoding.UTF8.GetString(msg);
            Console.WriteLine($"Topic: {e.ApplicationMessage.Topic}, Message: {value}");
            MessageReceivedEvent?.Invoke(e.ApplicationMessage.Topic, value);
        }
    }
}