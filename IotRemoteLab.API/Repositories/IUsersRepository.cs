using IotRemoteLab.Domain;

namespace IotRemoteLab.API.Repositories;

public interface IUsersRepository : IRepository<User>
{
    Task<Result<User?>> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
}