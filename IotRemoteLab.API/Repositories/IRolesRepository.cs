using IotRemoteLab.Domain.Role;

namespace IotRemoteLab.API.Repositories;

public interface IRolesRepository : IRepository<Role>
{
    Task<Result<Role?>> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}
