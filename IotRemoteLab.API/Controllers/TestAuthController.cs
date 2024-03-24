using System.Reflection;
using IotRemoteLab.Domain.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IotRemoteLab.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class TestAuthController: ControllerBase
{
    
    [HttpGet("simpe")]
    public async Task<ActionResult<string>> GetSimpePong()
    {
        var a = GetType().GetMethod(nameof(GetStudentPong)).GetCustomAttribute<AuthorizeAttribute>();
        var b = a.ToString();
        
        
        return Ok("pong");
    } 
    
    
    [Authorize(Roles = Roles.Student)]
    [HttpGet("student-ping")]
    public async Task<ActionResult<string>> GetStudentPong()
    {
        return Ok("pong");
    } 
    
    [Authorize(Roles = Roles.Admin)]
    [HttpGet("admin-ping")]
    public async Task<ActionResult<string>> GetAdminPong()
    {
        return Ok("pong");
    }  
    
}