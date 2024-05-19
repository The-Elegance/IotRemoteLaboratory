using IotRemoteLab.API.Repositories;
using IotRemoteLab.Domain;
using Microsoft.AspNetCore.Mvc;

namespace IotRemoteLab.API.Controllers;

[Route("api/[controller]")]
public class ScheduleController : ControllerBase
{
    private readonly IScheduleRepository _scheduleRepository;

    public ScheduleController(IScheduleRepository scheduleRepository )
    {
        _scheduleRepository = scheduleRepository;
    }

    [HttpGet("byTeam/{teamId:guid}")]
    public async Task<ActionResult<Schedule[]>> GetScheduleByTeam([FromRoute] Guid teamId)
    {
        var result = await _scheduleRepository.GetByTeamIdAsync(teamId);

        if (!result.IsSuccess)
            return result.StatusCode == null
                ? StatusCode(500, result.Error)
                : StatusCode(result.StatusCode.Value, result.Error);

        return result.Value;
    }
    
    [HttpGet("byStudent/{studentId:guid}")]
    public async Task<ActionResult<Schedule[]>> GetScheduleForUser([FromRoute] Guid studentId)
    {
        var result =  await _scheduleRepository.GetByUserIdAsync(studentId);
        
        if (!result.IsSuccess)
            return result.StatusCode == null
                ? StatusCode(500, result.Error)
                : StatusCode(result.StatusCode.Value, result.Error);

        return result.Value;
    }
    
    [HttpPost]
    public async Task<ActionResult<Schedule[]>> CreateSchedule([FromBody] CreateScheduleDto[] createSchedule)
    {
        var result = await _scheduleRepository.AddRangeAsync(createSchedule);
        
        if (!result.IsSuccess)
            return result.StatusCode == null
                ? StatusCode(500, result.Error)
                : StatusCode(result.StatusCode.Value, result.Error);

        return result.Value;
    }
}
