using System.Security.Claims;
using System.Security.Cryptography;
using IotRemoteLab.API.Repositories;
using IotRemoteLab.Domain.User;
using IotRemoteLab.Domain.User.Dtos;

namespace IotRemoteLab.API.Services;

public class AuthService : IAuthService
{
    private readonly IRolesRepository _rolesRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly IJwtService _jwtService;
    private readonly IConfiguration _configuration;
    private readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA512;
    
    
    public AuthService(IRolesRepository rolesRepository,IUsersRepository usersRepository, IJwtService jwtService, IConfiguration configuration)
    {
        _rolesRepository = rolesRepository;
        _usersRepository = usersRepository;
        _jwtService = jwtService;
        _configuration = configuration;
    }

    public async Task<Result<string>> RegisterUserAsync(RegisterUserDto registerUserDto)
    {
        var existed = await _usersRepository.GetUserByEmailAsync(registerUserDto.Email);
        
        if (existed != null)
            return Result.Fail<string>("Пользователь с такой почтой уже существует");
        
        //TODO: можно автомапер заюзать
        var user = await  ConvertDto(registerUserDto);
        
        await _usersRepository.AddAsync(user);

        return await LoginUserAsync(new LoginUserDto(registerUserDto.Email, registerUserDto.Password));
    }

    public async Task<Result<string>> LoginUserAsync(LoginUserDto loginUserDto)
    {
        var user = await _usersRepository.GetUserByEmailAsync(loginUserDto.Email);
        if (user == null)
            return Result.Fail<string>("Такого пользователя не существует");

        if (IsPasswordVerified(loginUserDto.Password, user.PasswordHash))
            return Result.Fail<string>("Неправильный пароль");

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
        };
        claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Name!)));

        return Result.Ok(_jwtService.GenerateAccessToken(claims));
    }
    
    private async Task<User> ConvertDto(RegisterUserDto userDto)
    {
        var role = await _rolesRepository.GetByNameAsync(Roles.Student);

        return new User
        {
            Id = Guid.NewGuid(),
            Email = userDto.Email,
            PasswordHash = Convert.ToHexString(GenerateHash(userDto.Password)),
            Name = userDto.Name,
            Surname = userDto.Surname,
            GroupNumber = userDto.GroupNumber,
            Roles = new[] { role }
        };
    }
    
    private bool IsPasswordVerified(string password, string hash)
    {
        return CryptographicOperations.FixedTimeEquals(GenerateHash(password), Convert.FromBase64String(hash));
    }

    private byte[] GenerateHash(string password)
    {
        var salt = _configuration.GetSection(ConfigPath.Salt).Value;
        var iteration = int.Parse(_configuration.GetSection(ConfigPath.NumberOfIterationForHash).Value!);
        var length = int.Parse(_configuration.GetSection(ConfigPath.HashLength).Value!);
        return  Rfc2898DeriveBytes.Pbkdf2(password.AsSpan(),
            Convert.FromBase64String(salt!), iteration, _hashAlgorithmName, length);
    } 
}