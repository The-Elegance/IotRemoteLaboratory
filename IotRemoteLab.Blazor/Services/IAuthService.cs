namespace IotRemoteLab.Blazor.Services;

public interface IAuthService
{
    Task<bool> Login(string email, string password);
    Task LogOut();
    Task<bool> Register(string login, string email, string password);
}