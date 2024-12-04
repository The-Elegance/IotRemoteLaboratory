using Asp.Versioning;
using IotRemoteLab.API.Services;
using IotRemoteLab.Application.User.Dtos;
using IotRemoteLab.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IotRemoteLab.API.Controllers;

[AllowAnonymous]
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }


    [HttpPost("login")]
    public async Task<ActionResult<string>> Login([FromBody] LoginUserDto loginUserDto)
    {
        var result = await _authService.LoginUserAsync(loginUserDto);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPost("register")]
    public async Task<ActionResult<string>> Register([FromBody] RegisterUserDto registerUserDto)
    {
        var result = await _authService.RegisterUserAsync(registerUserDto);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("profile/{id:guid}")]
    public async Task<User?> GetUserProfile(Guid id)
    {
        var user = await _authService.UserProfile(id);
        if (user != null) 
        {
            user.PasswordHash = null;
        }
        return user;
    }
}