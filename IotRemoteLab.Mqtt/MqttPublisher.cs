using IotRemoteLaboratory.Mqtt.Core;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;

namespace IotRemoteLab.Mqtt
{
    public sealed class MqttPublisher : MqttMember
    {
        public const string StaticClientId = "mqtt";

        public override IEnumerable<string> Topics { get; }
        protected override IMqttClient Client { get; }
        protected override IMqttClientOptions Options { get; }
        public bool IsStaticClientId { get; }


        public MqttPublisher(MqttParams mqttParams, bool isStaticClientId = true)
        {
            IsStaticClientId = isStaticClientId;

            Client = _mqttFactory.CreateMqttClient();

            var builder = new MqttClientOptionsBuilder()
                .WithClientId("Hel2x")
                .WithTcpServer(mqttParams.Ip, mqttParams.Port)
                .WithCleanSession();

            if (mqttParams.HasX509Certificates)
            {
                builder = builder.WithTls(new MqttClientOptionsBuilderTlsParameters() 
                {
                    UseTls = true,
                    SslProtocol = System.Security.Authentication.SslProtocols.Tls12,
                    Certificates = new[] 
                    {
                        mqttParams.CaX509Certificate, mqttParams.ClientX509Certificate
                    },
                    AllowUntrustedCertificates = true,
                    // без этой штуки ничего не работает !!!
                    IgnoreCertificateRevocationErrors = true
                });
            }

            Options = builder.Build();
            
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