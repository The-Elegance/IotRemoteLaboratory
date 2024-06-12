namespace IotRemoteLab.Blazor.Providers;

public interface IAccessTokenProvider
{
    Task<string?> GetToken();

    Dictionary<string, string> ParseToken(string token);

}