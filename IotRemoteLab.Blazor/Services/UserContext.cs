using IotRemoteLab.Domain;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace IotRemoteLab.Blazor.Services
{
    public class UserContext
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly HttpClient _httpClient;

        public UserContext(AuthenticationStateProvider authenticationStateProvider, HttpClient httpClient)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _httpClient = httpClient;
        }

        public async Task<User?> GetUserAsync() 
        {
            var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var id = state.User.FindFirst("Id");
            return await _httpClient.GetFromJsonAsync<User?>($"auth/profile/{id.Value}");
        }
    }
}
