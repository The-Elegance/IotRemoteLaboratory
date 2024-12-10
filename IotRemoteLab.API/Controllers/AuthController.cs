using Asp.Versioning;
using IotRemoteLab.API.Services;
using IotRemoteLab.Application.User.Dtos;
using IotRemoteLab.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IotRemoteLab.API.Controllers;

[Authorize(Roles = "Admin")]
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly Guid _userId;

    public AuthController(AuthService authService, IHttpContextAccessor httpContextAccessor)
    {
        _authService = authService;
        if (httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
        {
            _userId = Guid.Parse(
                httpContextAccessor.HttpContext.User.FindFirst("Id").Value
                );
        }
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<string>> Login([FromBody] LoginUserDto loginUserDto)
    {
        var result = await _authService.LoginUserAsync(loginUserDto);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<string>> Register([FromBody] RegisterUserDto registerUserDto)
    {
        var result = await _authService.RegisterUserAsync(registerUserDto);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [AllowAnonymous]
    [HttpPost("login/admin")]
    public async Task<ActionResult<string>> LoginAdmin([FromBody] LoginUserDto loginUserDto)
    {
        var result = await _authService.LoginAdminUserAsync(loginUserDto);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPost("user/confirm/verification/{id:guid}")]
    public async Task<IActionResult> ConfirmUserVerificationAsync(Guid id)
    {
        var result = await _authService.ConfirmVerification(_userId, id);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPost("user/confirm/admin/{id:guid}")]
    public async Task<IActionResult> ConfirmAdminUserAsync(Guid id)
    {
        var result = await _authService.ConfirmAdmin(_userId, id);
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