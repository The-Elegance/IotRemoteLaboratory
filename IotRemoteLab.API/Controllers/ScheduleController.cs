using System.Security.Claims;
using IotRemoteLab.API.Repositories;
using IotRemoteLab.Application;
using IotRemoteLab.Domain.Role;
using IotRemoteLab.Domain.Schedule;
using IotRemoteLab.Domain.Stand;
using IotRemoteLab.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IotRemoteLab.API.Controllers;

[Route("api/schedule")]
//[Authorize]
public class ScheduleController : ControllerBase
{
    private readonly ApplicationContext _context;
    public ScheduleController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult Add(ScheduleBase schedule)
    {
        _context.Add(schedule);
        _context.SaveChanges();

        return Ok();
    }


    [HttpPut]
    public async Task<ScheduleDto> UpdateSchedule()
    {
        throw new NotImplementedException();
    }
    
}
