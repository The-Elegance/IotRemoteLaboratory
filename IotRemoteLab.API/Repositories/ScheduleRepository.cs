using IotRemoteLab.Application;
using IotRemoteLab.Domain;
using IotRemoteLab.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IotRemoteLab.API.Repositories;

public class ScheduleRepository : IScheduleRepository
{
    private readonly ApplicationContext _applicationContext;
    
    public ScheduleRepository(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
    
    public async Task<Result<Schedule?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _applicationContext
            .Schedule
            .Include(p => p.Team)
            .Include(p => p.Stands)
            .SingleOrDefaultAsync(schedule => schedule.Id == id, cancellationToken: cancellationToken);
    }
    
    
    public async Task<Result<Schedule>> CreateOrUpdateAsync(Schedule entity, CancellationToken cancellationToken = default)
    {
        var result = await GetByIdAsync(entity.Id, cancellationToken);

        if (!result.IsSuccess)
        {
            await _applicationContext.Schedule.AddAsync(entity, cancellationToken);
            await _applicationContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        _applicationContext.Schedule.Update(entity);
        await _applicationContext.SaveChangesAsync(cancellationToken);
        
        return entity;
    }
    


    public async Task<Result<Schedule[]>> AddRangeAsync(IEnumerable<CreateScheduleDto> createScheduleDto, CancellationToken cancellationToken = default)
    {
        var createScheduleDtos = createScheduleDto as CreateScheduleDto[] ?? createScheduleDto.ToArray();
        
        foreach (var dto in createScheduleDtos)
        {
            if (!_applicationContext.Teams.Any(t => t.Id == dto.TeamId))
            {
                return Result.Fail<Schedule[]>($"TeamId={dto.TeamId} is not exist.", 400);
            }

            foreach (var standId in dto.Stands)
            {
                if (!await _applicationContext.Stands.AnyAsync(stand => stand.Id == standId,
                        cancellationToken: cancellationToken))
                {
                    return Result.Fail<Schedule[]>($"StandId={standId} is not exist.", 400);
                }
            }
        }
        
        var result = createScheduleDtos.Select(Map).ToArray();
        
        await AddRangeAsync(result, cancellationToken);
        return result;
    }
    
    public async Task<Result<Schedule[]>> GetByTeamIdAsync(Guid teamId, CancellationToken cancellationToken = default)
    {
        var a = _applicationContext.Schedule.Where(schedule =>
            schedule.Team.Id == teamId);
        return await a.ToArrayAsync(cancellationToken: cancellationToken);
    }

    public async Task<Result<Schedule[]>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var a = _applicationContext.Schedule.Where(schedule =>
            schedule.Team.Users.SingleOrDefault(user => user.Id == userId) != null);
        return await a.ToArrayAsync(cancellationToken: cancellationToken);
    }
    
    public async Task<Result<Schedule>> AddAsync(Schedule entity, CancellationToken cancellationToken = default)
    {
        await _applicationContext.Schedule.AddAsync(entity, cancellationToken);
     
        return entity;
    }
    
    public async Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await GetByIdAsync(id, cancellationToken);

        if (!result.IsSuccess)
            return Result.Fail<bool>("Not found", 404);

        _applicationContext.Schedule.Remove(result.Value!);
        
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
    
    private  Schedule Map(CreateScheduleDto dto)
    {
        return new Schedule
        {
            Id = Guid.NewGuid(),
            Team =  _applicationContext.Teams.Single(t => dto.TeamId == t.Id), 
            Stands = _applicationContext.Stands.Where(stand => dto.Stands.Any(id => id == stand.Id)).ToHashSet(),
            Start =  DateTimeOffset.FromUnixTimeSeconds(dto.Start).UtcDateTime, 
            End = DateTimeOffset.FromUnixTimeSeconds(dto.End).UtcDateTime
        };
    }
    
    private async Task<bool> AddRangeAsync(IEnumerable<Schedule> entity, CancellationToken cancellationToken = default)
    {
        await _applicationContext.Schedule.AddRangeAsync(entity, cancellationToken);
        await _applicationContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}