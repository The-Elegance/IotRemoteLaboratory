using IotRemoteLab.Application.User.Dtos;

namespace IotRemoteLab.Blazor.Services;

public interface IAuthService
{
    Task<bool> Login(string email, string password);
    
    Task LogOut();
    
    Task<bool> Register(RegisterUserDto registerUserDto);
}