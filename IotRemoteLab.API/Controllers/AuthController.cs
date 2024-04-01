using IotRemoteLab.API.Services;
using IotRemoteLab.Application.User.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IotRemoteLab.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
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
}