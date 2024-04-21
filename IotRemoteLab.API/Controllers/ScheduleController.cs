using System.Runtime.InteropServices;
using IotRemoteLab.API.Repositories;
using IotRemoteLab.Application;
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

    [HttpGet]
    public async Task<ScheduleDto[]> GetSchedule([FromBody] FindScheduleDto findScheduleDto)
    {
        
    } 
    
    [HttpGet($@"{{scheduleId:{nameof(Guid)}}}")]
    public async Task<ScheduleDto> GetScheduleById([FromBody] FindScheduleDto findScheduleDto, [FromRoute] Guid scheduleId)
    {
        
    }
    
    [HttpPost]
    public async Task<ScheduleDto[]> CreateSchedule([FromBody] CreateScheduleDto[] createSchedule)
    {
        var sch =  await _scheduleRepository.AddRangeAsync(createSchedule);
        
    }
    
    [HttpPut]
    public async Task<ScheduleDto> UpdateSchedule()
    {
        throw new NotImplementedException();
    }
}