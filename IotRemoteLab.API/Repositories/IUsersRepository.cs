using IotRemoteLab.Domain.User;

namespace IotRemoteLab.API.Repositories;

public interface IUsersRepository : IRepository<User>
{
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
}