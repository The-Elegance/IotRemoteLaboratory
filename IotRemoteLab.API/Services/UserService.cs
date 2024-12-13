using IotRemoteLab.API.Repositories;
using IotRemoteLab.Domain;
using IotRemoteLab.Persistence;
using Microsoft.EntityFrameworkCore;

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

        public Task<List<User>> GetUserByUniversity(Guid id) 
        {
            return _usersRepository._applicationContext.Users.Where(u => u.UniversityId == id).ToListAsync();
        }
    }
}
