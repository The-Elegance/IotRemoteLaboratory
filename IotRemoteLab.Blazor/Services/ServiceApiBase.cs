using IotRemoteLab.Blazor.Providers;

namespace IotRemoteLab.Blazor.Services;

public abstract class ServiceApiBase
{
    protected readonly HttpClient _client;
    protected readonly IAccessTokenProvider _accessTokenProvider;

    public ServiceApiBase(HttpClient client, IAccessTokenProvider accessTokenProvider)
    {
        _client = client;
        _accessTokenProvider = accessTokenProvider;
    }

    public async Task<HttpResponseMessage> SendAuthAsync(HttpRequestMessage request)
    {
        request.Headers.Add("Authorization",$"bearer {_accessTokenProvider.GetToken()}" );
        return await _client.SendAsync(request);
    }
    
}