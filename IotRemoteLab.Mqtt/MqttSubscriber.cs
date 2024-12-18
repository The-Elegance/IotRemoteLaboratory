﻿using System.Text;
using IotRemoteLaboratory.Mqtt.Core;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;

namespace IotRemoteLab.Mqtt
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
            var builder = new MqttClientOptionsBuilder()
                .WithClientId(Guid.NewGuid().ToString())
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
                        mqttParams.ClientX509Certificate, mqttParams.CaX509Certificate
                    },
                    AllowUntrustedCertificates = true,
                    // без этой штуки ничего не работает !!!
                    IgnoreCertificateRevocationErrors = true
                });
            }

            Options = builder.Build();


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

            msg ??= Array.Empty<byte>();

            var value = Encoding.UTF8.GetString(msg);
            Console.WriteLine($"Topic: {e.ApplicationMessage.Topic}, Message: {value}");
            MessageReceivedEvent?.Invoke(e.ApplicationMessage.Topic, value);
        }
    }
}