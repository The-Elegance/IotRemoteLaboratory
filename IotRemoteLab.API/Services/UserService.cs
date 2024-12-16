using IotRemoteLab.API.Repositories;
using IotRemoteLab.Domain;
using IotRemoteLab.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IotRemoteLab.API.Services
{
    public class UserService
    {
        private readonly UsersRepository _usersRepository;
        private readonly ApplicationContext _dbContext;

        public UserService(ApplicationContext dbContext, UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
            _dbContext = dbContext;
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

        public async Task<Team> GetTeamByUser(Guid id) 
        {
            var team = await _dbContext.Teams
                .Include(t => t.Members)
                .FirstOrDefaultAsync();
            
            team.Members = team.Members
                .Select(m => { m.PasswordHash = null; return m; })
                .ToList();

            return team;
        }
    }
}
