using IotRemoteLab.Domain.Role;
using IotRemoteLab.Domain.User;
using IotRemoteLab.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IotRemoteLab.API.Repositories;

public interface IRolesRepository : IRepository<Role>
{
    Task<Result<Role>> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}


public class RolesRepository : IRolesRepository
{
    private readonly ApplicationContext _applicationContext;

    public RolesRepository(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
    
    public async Task<Result<Role>> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _applicationContext.Roles.SingleOrDefaultAsync(role => role.Name == name,cancellationToken);
    }
    
    
    public async Task<Result<Role>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _applicationContext.Roles.SingleOrDefaultAsync(role => role.Id == id,cancellationToken);
    }

    public async  Task<Result<Role>> AddAsync(Role entity, CancellationToken cancellationToken = default)
    {
        await _applicationContext.Roles.AddAsync(entity, cancellationToken);
        await _applicationContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async  Task<Result<Role>> UpdateAsync(Role entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async  Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
