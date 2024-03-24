using IotRemoteLab.Domain.User.Dtos;

namespace IotRemoteLab.API.Services;

public interface IAuthService
{
    Task<Result<string>> RegisterUserAsync(RegisterUserDto registerUserDto);
    Task<Result<string>> LoginUserAsync(LoginUserDto loginUserDto);
}