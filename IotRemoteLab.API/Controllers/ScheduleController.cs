using Asp.Versioning;
using IotRemoteLab.API.Repositories;
using IotRemoteLab.Domain.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IotRemoteLab.API.Controllers;

[Authorize]
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/[controller]")]
public class ScheduleController : ControllerBase
{
    private readonly IScheduleRepository _scheduleRepository;

    public ScheduleController(IScheduleRepository scheduleRepository )
    {
        _scheduleRepository = scheduleRepository;
    }

    //[HttpGet("byTeam/{teamId:guid}")]
    //public async Task<ActionResult<ScheduleBase[]>> GetScheduleByTeam([FromRoute] Guid teamId)
    //{
    //    var result = await _scheduleRepository.GetByTeamIdAsync(teamId);

    //    if (!result.IsSuccess)
    //        return result.StatusCode == null
    //            ? StatusCode(500, result.Error)
    //            : StatusCode(result.StatusCode.Value, result.Error);

    //    return result.Value;
        
    //}
    
    //[HttpGet]
    //public async Task<ActionResult<ScheduleDto[]>> GetSchedule([FromBody] FindScheduleDto findScheduleDto)
    //{
    //    var result = await _scheduleRepository.FindSchedule(findScheduleDto);

    //    if (!result.IsSuccess)
    //        return StatusCode(result.StatusCode!.Value, result.Error);

    //    return Ok(result.Value);

    //}
    
    //[HttpGet("my")]
    //public async Task<ActionResult<ScheduleBase[]>> GetMySchedule()
    //{

    //    var userId = Guid.Parse(HttpContext.User.Claims.Single(claim => claim.Type == ClaimTypes.NameIdentifier ).Value);
        
    //    var result =  await _scheduleRepository.GetByUserIdAsync(userId);
        
    //    if (!result.IsSuccess)
    //        return result.StatusCode == null
    //            ? StatusCode(500, result.Error)
    //            : StatusCode(result.StatusCode.Value, result.Error);
        
    //    return result.Value;
    //}
    
    
    
    //[HttpGet("byStudent/{studentId:guid}")]
    //public async Task<ActionResult<ScheduleBase[]>> GetScheduleForUser([FromRoute] Guid studentId)
    //{
    //    var result =  await _scheduleRepository.GetByUserIdAsync(studentId);
        
    //    if (!result.IsSuccess)
    //        return result.StatusCode == null
    //            ? StatusCode(500, result.Error)
    //            : StatusCode(result.StatusCode.Value, result.Error);

    //    return result.Value;
    //}
    
    //[HttpPost]
    //[Authorize(Roles.Admin)]
    //public async Task<ActionResult<ScheduleBase[]>> CreateSchedule([FromBody] CreateScheduleDto[] createSchedule)
    //{
    //    var result = await _scheduleRepository.AddRangeAsync(createSchedule);
        
    //    if (!result.IsSuccess)
    //        return result.StatusCode == null
    //            ? StatusCode(500, result.Error)
    //            : StatusCode(result.StatusCode.Value, result.Error);

    //    return result.Value;
    //}
    
    
    [HttpPut]
    public async Task<ScheduleDto> UpdateSchedule()
    {
        throw new NotImplementedException();
    }

    [Authorize(Roles = Roles.Student)]
    [HttpPost("take")]
    public Task<IActionResult> TakeTime() 
    {
        throw new NotImplementedException();
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost("class")]
    public Task<IActionResult> AddClass() 
    {
        throw new NotImplementedException();
    }
}
