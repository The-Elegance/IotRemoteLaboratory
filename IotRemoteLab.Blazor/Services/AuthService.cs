using System.Net.Http;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using IotRemoteLab.Application.User.Dtos;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IotRemoteLab.Blazor.Services;

public class AuthService :  IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _storageService;

    public AuthService(HttpClient client, ILocalStorageService storageService )
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

    public async Task<bool> Register(RegisterUserDto registerUserDto)
    {
        var message = new HttpRequestMessage(HttpMethod.Post, "api/Auth/register");
        message.Content = JsonContent.Create(registerUserDto);

        var response = await _httpClient.SendAsync(message);

        if (!response.IsSuccessStatusCode)
            return false;

        var token = await response.Content.ReadAsStringAsync();

        await _storageService.SetItemAsync(StorageFieldNames.TokenName, token);

        return true;
    }
}