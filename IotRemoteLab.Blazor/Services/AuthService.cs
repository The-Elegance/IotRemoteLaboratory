using System.Net;
using System.Net.Http.Json;
using IotRemoteLab.Application.User.Dtos;
using IotRemoteLab.Blazor.Services.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace IotRemoteLab.Blazor.Services;

public class AuthService :  IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _storageService;
    private readonly NavigationManager _navigationManager;


    public AuthService(HttpClient client, ILocalStorageService storageService, NavigationManager navigationManager)
    {
        _httpClient = client;
        _storageService = storageService;
    }


    public async Task<bool> Login(string email, string password)
    {
		var response = await _httpClient.PostAsJsonAsync("api/Auth/login/", new LoginUserDto(email, password));

		if (!response.IsSuccessStatusCode)
            return false;

        var token = await response.Content.ReadAsStringAsync();

        await _storageService.SetItemAsync(StorageFieldNames.TokenName, token);
        return true;
    }

    public async Task LogOut()
    {
        await _storageService.RemoveItemAsync(StorageFieldNames.TokenName);
    }

    public async Task<bool> Register(string login, string email, string password)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/register", new { login = login, email = email, password = password });

        if (response.StatusCode != HttpStatusCode.OK)
            return false;

        var token = await response.Content.ReadAsStringAsync();
        await _storageService.SetItemAsync(StorageFieldNames.TokenName, token);

        return true;
    }
}