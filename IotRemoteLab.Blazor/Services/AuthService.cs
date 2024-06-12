using System.Net.Http.Json;
using System.Text.Json;
using Blazored.LocalStorage;
using IotRemoteLab.Application.User.Dtos;

namespace IotRemoteLab.Blazor.Services;

public class AuthService :  IAuthService
{
    private readonly HttpClient _client;
    private readonly ILocalStorageService _storageService;

    public AuthService(HttpClient client, ILocalStorageService storageService )
    {
        _client = client;
        _storageService = storageService;
    }
    
    
    public async Task<bool> Login(string email, string password)
    {
        
        Console.WriteLine("noerror 29");
        Console.WriteLine(_client.BaseAddress.ToString());
        
        var message = new HttpRequestMessage(HttpMethod.Post, "api/Auth/login");
        message.Content = JsonContent.Create(new { email = email, password=password });
        
        Console.WriteLine("Body");
        
        Console.WriteLine($"{email}, {password}");
        
        var response = await _client.SendAsync(message);
        
        if (!response.IsSuccessStatusCode)
            return false;
        
        Console.WriteLine("noerror 39");
        var token = await response.Content.ReadAsStringAsync();
        
        Console.WriteLine(token);
        
        await _storageService.SetItemAsync(StorageFieldNames.TokenName, token);

        
        Console.WriteLine("noerror 47");
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

        var response = await _client.SendAsync(message);

        if (!response.IsSuccessStatusCode)
            return false;

        var token = await response.Content.ReadAsStringAsync();

        await _storageService.SetItemAsync(StorageFieldNames.TokenName, token);

        return true;
    }
}