using IotRemoteLab.Domain.Stand;
using System.Text.Json.Serialization;

namespace IotRemoteLab.API.Services
{
    public class StandsService
    {
        private readonly Dictionary<Guid, StandDeltaData> _deltaDataByStandId = [];

        public StandsService() 
        {
            
        }

        public void AddDeltaData(Guid id, string? code, string? debugUploadOutput) 
        {
            // Immutable or Not?
            _deltaDataByStandId[id] = new StandDeltaData(code, debugUploadOutput);
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
