using IotRemoteLab.Domain.Stand;
using IotRemoteLaboratory.Mqtt.Core;
using System.Text.Json.Serialization;

namespace IotRemoteLab.API.Services
{
    public class StandsService
    {
        private readonly Dictionary<Guid, StandDeltaData> _deltaDataByStandId = [];

        private MqttPublisher _mqttPublisher;

        public StandsService(MqttPublisher mqttPublisher) 
        {
            _mqttPublisher = mqttPublisher;
        }

        public void PublishMessageAsync(string topic, string payload) 
        {
            _mqttPublisher.PublishMessageAsync(topic, payload);
        }

        public void AddDeltaData<T>(Guid id, T code, string? debugUploadOutput) 
        {
            // Immutable or Not?
            //_deltaDataByStandId[id] = new StandDeltaData(code, debugUploadOutput);
        }

        public StandDeltaData GetDeltaData(Guid id) 
        {
            if (_deltaDataByStandId.TryGetValue(id, out var standDeltaData)) {
                return standDeltaData;
            }

            return new StandDeltaData(null, null);
        }
    }
}
