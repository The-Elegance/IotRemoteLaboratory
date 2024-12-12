using System.Security.Claims;
using System.Security.Cryptography;
using IotRemoteLab.API.Repositories;
using IotRemoteLab.Application.User.Dtos;
using IotRemoteLab.Domain;
using IotRemoteLab.Domain.Role;

namespace IotRemoteLab.API.Services;

public class AuthService
{
    private readonly RolesRepository _rolesRepository;
    private readonly UsersRepository _usersRepository;
    private readonly IJwtService _jwtService;
    private readonly IConfiguration _configuration;
    private readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA512;


    #region Constructors


    public AuthService(RolesRepository rolesRepository, UsersRepository usersRepository, IJwtService jwtService, IConfiguration configuration)
    {
        _rolesRepository = rolesRepository;
        _usersRepository = usersRepository;
        _jwtService = jwtService;
        _configuration = configuration;
    }


    #endregion Constructors


    #region Authorization Methods

    public async Task<Result<string>> RegisterUserAsync(RegisterUserDto registerUserDto)
    {
        var existed = await _usersRepository.GetUserByEmailAsync(registerUserDto.Email);

        if (!existed.IsSuccess)
            return Result.Fail<string>("Пользователь с такой почтой уже существует");

        //TODO: можно автомапер заюзать
        var user = await ConvertDto(registerUserDto);
        await _usersRepository.AddAsync(user);

        return await LoginUserAsync(new LoginUserDto(registerUserDto.Email, registerUserDto.Password));
    }

    public async Task<Result<string>> LoginUserAsync(LoginUserDto loginUserDto)
    {
        var res = await _usersRepository.GetUserByEmailAsync(loginUserDto.Email);

        var user = res.Value;

        if (user == null || !user.IsVerified)
            return Result.Fail<string>("Такого пользователя не существует");

        if (IsPasswordVerified(loginUserDto.Password, user.PasswordHash))
            return Result.Fail<string>("Неправильный пароль");

        var claims = new List<Claim>
        {
            new Claim("Id", user.Id.ToString()),
        };
        claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Name)));

        return Result.Ok(_jwtService.GenerateAccessToken(claims));
    }

    public async Task<Result<string>> LoginAdminUserAsync(LoginUserDto loginUserDto) 
    {
        var res = await _usersRepository.GetUserByEmailAsync(loginUserDto.Email);

        var user = res.Value;

        if (user == null || !user.IsAdmin)
            return Result.Fail<string>("Такого пользователя не существует");

        if (IsPasswordVerified(loginUserDto.Password, user.PasswordHash))
            return Result.Fail<string>("Неправильный пароль");

        var claims = new List<Claim>
        {
            new Claim("Id", user.Id.ToString()),
        };
        claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Name)));

        return Result.Ok(_jwtService.GenerateAccessToken(claims));
    }


    #endregion Authorization Methods


    #region Un/confirm Methods


    public async Task<Result<string>> ConfirmVerification(Guid who, Guid whom)
    {
       var adminUser = await _usersRepository.GetUserById(who);

        if (adminUser == null)
            return Result.Fail<string>("У вас недостаточно прав.");

        var user = await _usersRepository.GetUserById(whom);

        if (user == null)
            return Result.Fail<string>("Такого пользователя не существует");


        if (user.IsVerified)
            return Result.Fail<string>("Данный пользователь уже имеет верификацию");

        user.IsVerified = true;

        await _usersRepository.CreateOrUpdateAsync(user);

        return Result.Ok("Успешно");
    }

    public async Task<Result<string>> ConfirmAdmin(Guid who, Guid whom)
    {
        var adminUser = await _usersRepository.GetUserById(who);

        if (adminUser == null)
            return Result.Fail<string>("У вас недостаточно прав.");

        var user = await _usersRepository.GetUserById(whom);

        if (user == null || !user.IsAdmin)
            return Result.Fail<string>("Такого пользователя не существует");

        if (user.IsVerified)
            return Result.Fail<string>("Данный пользователь уже является администратором");

        var role = await _rolesRepository.GetByNameAsync(Roles.Student);

        user.Roles = new List<Role>(user.Roles)
        {
            role.Value
        };
        user.IsAdmin = true;
        user.IsVerified = true;

        await _usersRepository.CreateOrUpdateAsync(user);

        return Result.Ok("Успешно");
    }

    public async Task<Result<string>> UnconfirmVerification(Guid id)
    {
        var user = await _usersRepository.GetUserById(id);

        if (user == null)
            return Result.Fail<string>("Такого пользователя не существует");

        if (user.IsVerified)
            return Result.Fail<string>("Данный пользователь не имеет верификацию");

        user.IsVerified = false;

        await _usersRepository.CreateOrUpdateAsync(user);

        return Result.Ok("Успешно");
    }

    public async Task<Result<string>> UnconfirmAdmin(Guid id)
    {
        var user = await _usersRepository.GetUserById(id);

        if (user == null)
            return Result.Fail<string>("Такого пользователя не существует");

        if (!user.IsVerified)
            return Result.Fail<string>("Данный пользователь не является администратором");

        var userRoles = new List<Role>(user.Roles);
        userRoles.Remove(userRoles.First(r => r.Name == Roles.Admin));

        user.IsAdmin = false;
        user.IsVerified = false;

        await _usersRepository.CreateOrUpdateAsync(user);

        return Result.Ok("Успешно");
    }


    #endregion Un/confirm Methods


    #region Private Methods


    private async Task<User> ConvertDto(RegisterUserDto userDto)
    {
        var role = await _rolesRepository.GetByNameAsync(Roles.Student);

        return new User
        {
            Email = userDto.Email,
            Name = userDto.Name,
            Surname = userDto.Surname,
            MiddleName = userDto.MiddleName,
            UniversityId = userDto.UniversityId,
            AcademyGroupId = userDto.AcademyGroupId,
            PasswordHash = Convert.ToHexString(GenerateHash(userDto.Password)),
            Roles = [role.Value] //new[] { role.Value }!
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
        return Rfc2898DeriveBytes.Pbkdf2(password.AsSpan(),
            Convert.FromBase64String(salt!), iteration, _hashAlgorithmName, length);
    }


    #endregion Private Methods
}