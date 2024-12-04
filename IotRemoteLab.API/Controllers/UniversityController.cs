using Asp.Versioning;
using IotRemoteLab.Domain;
using IotRemoteLab.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IotRemoteLab.API.Controllers
{
    [ApiVersion(1.0)]
    //[Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UniversityController : ControllerBase
    {
        private readonly ApplicationContext _dbContext;

        public UniversityController(ApplicationContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        public Task<List<University>> GetUniversities()
        {
            return _dbContext.Universities.ToListAsync();
        }

        [HttpGet("{id:guid}")]
        public Task<University?> GetUniversity(Guid id)
        {
            return _dbContext.Universities.FirstOrDefaultAsync(u => u.Id == id);
        }

        [HttpGet("academygroups")]
        public Task<List<AcademyGroup>> GetAcademyGroups()
        {
            return _dbContext.AcademyGroup.ToListAsync();
        }

        [HttpGet("{id:guid}/academygroups")]
        public async Task<List<AcademyGroup>> GetAcademyGroupsByUniversityId(Guid id)
        {
            var university = await _dbContext.Universities
                .Include(u => u.Groups)
                .FirstOrDefaultAsync(u => u.Id == id);

            return university.Groups;
        }

        [HttpGet("academygroup/{id:guid}")]
        public Task<AcademyGroup?> GetAcademyGroup(Guid id)
        {
            return _dbContext.AcademyGroup.FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
