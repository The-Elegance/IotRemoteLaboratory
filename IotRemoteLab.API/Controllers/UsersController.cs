using Asp.Versioning;
using IotRemoteLab.Domain.User;
using IotRemoteLab.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IotRemoteLab.API.Controllers
{
    [ApiVersion(1.0)]
    //[Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsersController : ControllerBase
    {
        private ApplicationContext _context;


        public UsersController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetUser()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
