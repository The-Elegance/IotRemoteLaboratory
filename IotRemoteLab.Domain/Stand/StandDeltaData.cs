using System.Text.Json.Serialization;

namespace IotRemoteLab.Domain.Stand
{
    [method: JsonConstructor]
    public readonly struct StandDeltaData(string? code, string? debugUploadOutput)
    {
        public string Code { get; } = code ?? string.Empty;
        public string DebugUploadOutput { get; } = debugUploadOutput ?? string.Empty;
    }
}
