using IotRemoteLab.API.Controllers;
using IotRemoteLab.Domain;

namespace IotRemoteLab.API.Repositories;

public interface IScheduleRepository : IRepository<Schedule>
{
    Task<Result<Schedule[]>> AddRangeAsync(IEnumerable<CreateScheduleDto> createScheduleDto, CancellationToken cancellationToken = default);

    Task<Result<Schedule[]>> GetByTeamIdAsync(Guid teamId, CancellationToken cancellationToken = default);

    Task<Result<Schedule[]>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

}