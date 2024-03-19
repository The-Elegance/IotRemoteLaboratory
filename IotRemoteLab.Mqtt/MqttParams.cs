namespace IotRemoteLaboratory.Mqtt.Core
{
    public sealed class MqttParams
    {
        public string Ip { get; }
        public int Port { get; }
        public IEnumerable<string> Topics { get; }

        public MqttParams(string ip, int port, IEnumerable<string> topics)
        {
            Ip = ip;
            Port = port;
            Topics = topics;
        }

        public MqttParams(string ip, int port, params string[] topics) : this(ip, port, (IEnumerable<string>)topics)
        {

        }
    }
}