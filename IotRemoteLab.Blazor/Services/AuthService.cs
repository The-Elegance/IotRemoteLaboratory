using System.Net;
using System.Net.Http.Json;
using IotRemoteLab.Application.User.Dtos;
using IotRemoteLab.Blazor.Models;
using IotRemoteLab.Blazor.Services.LocalStorage;

namespace IotRemoteLab.Blazor.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _storageService;


    public AuthService(HttpClient client, ILocalStorageService storageService)
    {
        _httpClient = client;
        _storageService = storageService;
    }


    public async Task<bool> Login(string email, string password)
    {
		var response = await _httpClient.PostAsJsonAsync("auth/login/", new LoginUserDto(email, password));

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

    public async Task<bool> Register(RegistrationUserData registrationUserData)
    {
        var response = await _httpClient.PostAsJsonAsync("auth/register", registrationUserData);

        if (response.StatusCode != HttpStatusCode.OK)
            return false;

        var token = await response.Content.ReadAsStringAsync();
        await _storageService.SetItemAsync(StorageFieldNames.TokenName, token);

        return true;
    }
}