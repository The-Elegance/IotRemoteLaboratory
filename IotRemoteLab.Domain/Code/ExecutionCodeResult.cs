using System.Text.Json.Serialization;

namespace IotRemoteLab.Domain.Code
{
    [method: JsonConstructor]
    public readonly struct ExecutionCodeResult(Guid standId, Guid editorElementId, string output)
    {
        public Guid StandId { get; }
        public string Output { get; }
    }
}
