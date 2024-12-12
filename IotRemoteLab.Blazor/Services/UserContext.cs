using IotRemoteLab.Common.Services.LocalStorage;
using IotRemoteLab.Domain;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace IotRemoteLab.Blazor.Services
{
    public class UserContext
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        public UserContext(AuthenticationStateProvider authenticationStateProvider, HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public async Task<User?> GetUserProfileAsync() 
        {
            return await _httpClient.GetFromJsonAsync<User?>($"User/profile");
        }
    }
}
