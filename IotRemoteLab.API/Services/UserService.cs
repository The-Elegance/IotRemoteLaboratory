using IotRemoteLab.API.Repositories;
using IotRemoteLab.Domain;

namespace IotRemoteLab.API.Services
{
    public class UserService
    {
        private readonly UsersRepository _usersRepository;

        public UserService(UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public Task<User?> UserProfileAsync(Guid id)
        {
            return _usersRepository.GetUserById(id);
        }

        public async Task<List<User>> GetUsersAsync() 
        {
            var users = await _usersRepository.GetUsersAsync();
            return users.Select(u => { u.PasswordHash = string.Empty; return u; }).ToList();
        }
    }
}
