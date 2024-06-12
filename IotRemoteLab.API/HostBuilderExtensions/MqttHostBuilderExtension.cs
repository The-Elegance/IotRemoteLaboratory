using IotRemoteLab.Mqtt;

namespace IotRemoteLab.API.HostBuilderExtensions
{
    public static class MqttHostBuilderExtension
    {
        public static IApplicationBuilder UseMqtt(this IApplicationBuilder app, params Action<string, string>[] receivedMessageActions) 
        {
            var publisher = app.ApplicationServices.GetRequiredService<MqttPublisher>();
            publisher.Connect();
            var subscriber = app.ApplicationServices.GetRequiredService<MqttSubscriber>();
            subscriber.Connect();

            if (receivedMessageActions == null || receivedMessageActions.Length == 0)
            {
                foreach (var act in receivedMessageActions)
                {
                    subscriber.MessageReceivedEvent += act;
                }
            }

            return app;
        }

        public static IApplicationBuilder UseMqtt(this IApplicationBuilder app)
        {
            return UseMqtt(app, []);
        }
    }
}
