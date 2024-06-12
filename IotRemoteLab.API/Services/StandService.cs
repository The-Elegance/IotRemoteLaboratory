using IotRemoteLab.API.CLI;
using IotRemoteLab.Domain.Stand;
using IotRemoteLab.Mqtt;

namespace IotRemoteLab.API.Services
{
    public class StandsService
    {
        private readonly Dictionary<Guid, StandDeltaData> _deltaDataByStandId = [];

        private MqttPublisher _mqttPublisher;
        private ICommandExecutor _commandExecutor;

        public StandsService(MqttPublisher mqttPublisher, ICommandExecutor commandExecutor) 
        {
            _mqttPublisher = mqttPublisher;
            _commandExecutor = commandExecutor;
        }

        public void ExecuteCommand(Guid id, string cmd) 
        {
            var cmdParts = cmd.Split();

            var command = cmdParts.ToList();
            command.Add(id.ToString());

            _commandExecutor.Execute([.. command]);
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
