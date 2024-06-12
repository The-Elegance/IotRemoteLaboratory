using Blazored.LocalStorage;

namespace IotRemoteLab.Blazor.Providers;

public class AccessTokenProvider : IAccessTokenProvider
{
    private readonly ILocalStorageService _localStorageService;

    public AccessTokenProvider(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    } 
    
    public async Task<string?> GetToken()
    {
        return await _localStorageService.GetItemAsync<string>("token");
    }

    public Dictionary<string, string> ParseToken(string token)
    {
        return JWTDecoder.Decoder.DecodePayload<Dictionary<string, string>>(token);
    }
}