using IotRemoteLab.Domain.Role;
using IotRemoteLab.Domain.User;

namespace IotRemoteLab.API.Repositories;

public interface IRolesRepository : IRepository<Role>
{
    Task<Result<Role?>> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}