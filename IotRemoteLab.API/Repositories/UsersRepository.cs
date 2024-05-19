using IotRemoteLab.Domain.User;
using IotRemoteLab.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IotRemoteLab.API.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly ApplicationContext _applicationContext;

    public UsersRepository(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }


    public async Task<Result<User?>> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _applicationContext
                .Users
                .Include(u => u.Roles)
                .SingleOrDefaultAsync(user => user.Id == id, cancellationToken)
            ;
    }

    public async Task<Result<bool>> AddAsync(User entity, CancellationToken cancellationToken = default)
    {
        var user = await _applicationContext
            .Users
            .SingleOrDefaultAsync(user => user.Id == entity.Id,
                cancellationToken);

        if (user != null)
            return false;
        
        await _applicationContext.Users.AddAsync(entity, cancellationToken);
        await _applicationContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<Result<User>> UpdateAsync(User entity, CancellationToken cancellationToken = default)
    {
        var user = await GetAsync(entity.Id, cancellationToken);

        if (user.Value == null)
        {
            await _applicationContext.Users.AddAsync(entity, cancellationToken);
            await _applicationContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        _applicationContext.Users.Update(entity);
        await _applicationContext.SaveChangesAsync(cancellationToken);
        
        return entity;
    }
    
    public async Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await GetAsync(id, cancellationToken);

        if (user.Value == null)
            return false;

        _applicationContext.Users.Remove(user.Value);
        await _applicationContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<Result<User?>> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _applicationContext
                .Users
                .Include(u => u.Roles)
                .SingleOrDefaultAsync(user => user.Email == email, cancellationToken);
    }
}