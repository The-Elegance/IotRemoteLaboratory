using IotRemoteLaboratory.Mqtt.Core;

namespace IotRemoteLab.API.HostBuilderExtentions
{
    public static class MqttServiceCollectionExtensions
    {
#if DEBUG
        public static IServiceCollection AddMqtt(this IServiceCollection services, params string[] topics)
        {
            return AddMqtt(services, new MqttParams("test.mosquitto.org", 1883, topics));
        }
#endif

        public static IServiceCollection AddMqtt(this IServiceCollection services, string ip, int port, string[] topics)
        {
            return AddMqtt(services, new MqttParams(ip, port, topics));
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
