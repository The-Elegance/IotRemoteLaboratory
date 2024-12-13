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
