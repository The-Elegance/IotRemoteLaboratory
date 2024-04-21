using System.ComponentModel.DataAnnotations;
using IotRemoteLab.API.Repositories;
using IotRemoteLab.Domain;
using IotRemoteLab.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IotRemoteLab.API.Controllers;

[Route("api/[controller]")]
public class ScheduleController : ControllerBase
{
    private readonly IScheduleRepository _scheduleRepository;

    public ScheduleController(IScheduleRepository scheduleRepository )
    {
        _scheduleRepository = scheduleRepository;
    }

    [HttpGet()]
    public async Task<ScheduleDto[]> GetSchedule()
    {
        throw new NotImplementedException();
    }
    
    [HttpGet()]
    public async Task<ScheduleDto[]> CreateSchedule([FromBody] CreateScheduleDto[] createSchedule)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet()]
    public async Task<ScheduleDto> UpdateSchedule()
    {
        throw new NotImplementedException();
    }
    
    
}







public class ScheduleDto
{
    [Required] public Guid TeamId { get; set; }
    
    [Required] public Guid[] Stands { get; init; }
    
    [Required] public long Start { get; init; }
    [Required] public long End { get; init; }
}

public record CreateScheduleDto
{
    [Required] public Guid TeamId { get; set; }
    
    [Required] public Guid[] Stands { get; init; }
    
    [Required] public long Start { get; init; }
    [Required] public long End { get; init; }
}

public interface IScheduleRepository : IRepository<Schedule>
{
    Task<bool> AddRangeAsync(IEnumerable<Schedule> entity, CancellationToken cancellationToken = default);
    Task<bool> AddRangeAsync(IEnumerable<CreateScheduleDto> createScheduleDto, CancellationToken cancellationToken = default);
}

public class ScheduleRepository : IScheduleRepository
{
    private readonly ApplicationContext _applicationContext;

    public ScheduleRepository(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }


    public async Task<Schedule?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _applicationContext
            .Schedule
            .Include(p => p.Team)
            .Include(p => p.Stands)
            .SingleOrDefaultAsync(schedule => schedule.Id == id, cancellationToken: cancellationToken);
    }

    public async Task<bool> AddAsync(Schedule entity, CancellationToken cancellationToken = default)
    {
        await _applicationContext.Schedule.AddAsync(entity, cancellationToken);
     
        return true;
    }

    public async Task<Schedule> UpdateAsync(Schedule entity, CancellationToken cancellationToken = default)
    {
        var user = await GetAsync(entity.Id, cancellationToken);

        if (user == null)
        {
            await _applicationContext.Schedule.AddAsync(entity, cancellationToken);
            await _applicationContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        _applicationContext.Schedule.Update(entity);
        await _applicationContext.SaveChangesAsync(cancellationToken);
        
        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var schedule = await GetAsync(id, cancellationToken);

        if (schedule == null)
            return false;

        _applicationContext.Schedule.Remove(schedule);
        await _applicationContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> AddRangeAsync(IEnumerable<Schedule> entity, CancellationToken cancellationToken = default)
    {
        await _applicationContext.Schedule.AddRangeAsync(entity, cancellationToken);
        return true;
    }

    public async Task<bool> AddRangeAsync(IEnumerable<CreateScheduleDto> createScheduleDto, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}