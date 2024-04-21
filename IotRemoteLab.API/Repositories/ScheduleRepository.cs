using IotRemoteLab.API.Controllers;
using IotRemoteLab.Application;
using IotRemoteLab.Domain;
using IotRemoteLab.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IotRemoteLab.API.Repositories;

public class ScheduleRepository : IScheduleRepository
{
    private readonly ApplicationContext _applicationContext;
    private readonly IScheduleConverterDto _converterDto;

    public ScheduleRepository(ApplicationContext applicationContext, IScheduleConverterDto converterDto)
    {
        _applicationContext = applicationContext;
        _converterDto = converterDto;
    }


    public async Task<Result<Schedule>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _applicationContext
            .Schedule
            .Include(p => p.Team)
            .Include(p => p.Stands)
            .SingleOrDefaultAsync(schedule => schedule.Id == id, cancellationToken: cancellationToken);
    }

    public async Task<Result<Schedule>> AddAsync(Schedule entity, CancellationToken cancellationToken = default)
    {
        await _applicationContext.Schedule.AddAsync(entity, cancellationToken);
     
        return true;
    }

    public async Task<Result<Schedule>> UpdateAsync(Schedule entity, CancellationToken cancellationToken = default)
    {
        var user = await GetByIdAsync(entity.Id, cancellationToken);

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

    public async Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var schedule = await GetByIdAsync(id, cancellationToken);

        if (schedule == null)
            return false;

        _applicationContext.Schedule.Remove(schedule);
        await _applicationContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<Result<IEnumerable<Schedule>>> FindSchedule(FindScheduleDto findScheduleDto, CancellationToken cancellationToken = default)
    {
        var res = await   _applicationContext
            .Schedule
            .Include(schedule =>  schedule.Stands)
            .Include(schedule =>  schedule.Team)
            .Where(schedule => (findScheduleDto.StandId == null ||
                                schedule.Stands.Any(stand => stand.Id == findScheduleDto.StandId))
                                && (findScheduleDto.TeamId == null ||
                                    schedule.Team.Id == findScheduleDto.TeamId)
                                && (findScheduleDto.End == null ||
                                    schedule.End ==  new DateTime(findScheduleDto.End.Value))
                                && (findScheduleDto.Start == null ||
                                    schedule.Start ==  new DateTime(findScheduleDto.Start.Value)))
            .ToArrayAsync(cancellationToken);

        return res;
    }

    public async Task<Result<IEnumerable<Schedule>>> AddRangeAsync(IEnumerable<Schedule> entity, CancellationToken cancellationToken = default)
    {
        await _applicationContext.Schedule.AddRangeAsync(entity, cancellationToken);
        return true;
    }

    public async Task<Result<IEnumerable<Schedule>>> AddRangeAsync(IEnumerable<CreateScheduleDto> createScheduleDto, CancellationToken cancellationToken = default)
    {
        return await AddRangeAsync(createScheduleDto.Select(_converterDto.Convert), cancellationToken);
    }
}


public interface IScheduleConverterDto
{
    Schedule Convert(CreateScheduleDto createScheduleDto);
}

