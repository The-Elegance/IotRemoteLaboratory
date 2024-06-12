using IotRemoteLaboratory.Mqtt.Core;
using System.Security.Cryptography.X509Certificates;
using IotRemoteLab.Mqtt;

namespace IotRemoteLab.API.HostBuilderExtensions
{
    public static class MqttServiceCollectionExtension
    {
        public static IServiceCollection AddMqtt(this IServiceCollection services, string ip, int port, string[] topics)
        {
            return AddMqtt(services, new MqttParams(ip, port, topics));
        }

        public static IServiceCollection AddMqtt(this IServiceCollection services, string ip, int port, X509Certificate ca, X509Certificate2 client, string[] topics)
        {
            return AddMqtt(services, new MqttParams(ip, port, ca, client, topics));
        }

        public static IServiceCollection AddMqtt(this IServiceCollection services, MqttParams mqttParams) 
        {
            services.AddSingleton(_ => mqttParams);
            services.AddSingleton<MqttPublisher>();
            services.AddSingleton<MqttSubscriber>();

            return services;
        }
    }
}
