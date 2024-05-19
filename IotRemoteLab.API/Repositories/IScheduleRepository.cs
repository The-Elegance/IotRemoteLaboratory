using IotRemoteLab.API.Controllers;
using IotRemoteLab.Application;
using IotRemoteLab.Domain;

namespace IotRemoteLab.API.Repositories;

public interface IScheduleRepository : IRepository<Schedule>
{
    Task<Result<Schedule[]>> AddRangeAsync(IEnumerable<CreateScheduleDto> createScheduleDto, CancellationToken cancellationToken = default);

    Task<Result<Schedule[]>> GetByTeamIdAsync(Guid teamId, CancellationToken cancellationToken = default);

    Task<Result<Schedule[]>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<Result<IEnumerable<Schedule>>> FindSchedule(FindScheduleDto findScheduleDto, CancellationToken cancellationToken = default);
    
    Task<Result<IEnumerable<Schedule>>> AddRangeAsync(IEnumerable<Schedule> entity, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<Schedule>>> AddRangeAsync(IEnumerable<CreateScheduleDto> createScheduleDto, CancellationToken cancellationToken = default);
}