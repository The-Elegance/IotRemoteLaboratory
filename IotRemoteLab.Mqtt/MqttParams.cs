using System.Security.Cryptography.X509Certificates;

namespace IotRemoteLaboratory.Mqtt.Core
{
    public sealed class MqttParams(string ip, int port, IEnumerable<string> topics)
    {
        public string Ip { get; } = ip;
        public int Port { get; } = port;
        public IEnumerable<string> Topics { get; } = topics;
        public bool HasX509Certificates { get; }
        public X509Certificate? CaX509Certificate { get; }
        public X509Certificate2? ClientX509Certificate { get; }


        #region Constructors


        public MqttParams(string ip, int port, X509Certificate ca, X509Certificate2 client, IEnumerable<string> topics) : this(ip, port, topics)
        {
            CaX509Certificate = ca;
            ClientX509Certificate = client;

            ArgumentNullException.ThrowIfNull(ca);
            ArgumentNullException.ThrowIfNull(client);

            HasX509Certificates = true;
        }


        public MqttParams(string ip, int port, params string[] topics) : this(ip, port, (IEnumerable<string>)topics)
        {

        }


        #endregion Constructors
    }
}