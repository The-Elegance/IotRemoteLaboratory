using Asp.Versioning;
using IotRemoteLab.Domain;
using IotRemoteLab.Domain.Role;
using IotRemoteLab.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IotRemoteLab.API.Controllers
{
    [ApiVersion(1.0)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UniversityController : ControllerBase
    {
        private readonly ApplicationContext _dbContext;


        public UniversityController(ApplicationContext context)
        {
            _dbContext = context;
        }


        [AllowAnonymous]
        [HttpGet]
        public Task<List<University>> GetUniversities()
        {
            return _dbContext.Universities.ToListAsync();
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public Task<University?> GetUniversity(Guid id)
        {
            return _dbContext.Universities.FirstOrDefaultAsync(u => u.Id == id);
        }

        [AllowAnonymous]
        [HttpGet("academygroups")]
        public Task<List<AcademyGroup>> GetAcademyGroups()
        {
            return _dbContext.AcademyGroup.ToListAsync();
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}/academygroups")]
        public async Task<List<AcademyGroup>> GetAcademyGroupsByUniversityId(Guid id)
        {
            var university = await _dbContext.Universities
                .Include(u => u.Groups)
                .FirstOrDefaultAsync(u => u.Id == id);

            return university.Groups;
        }

        [AllowAnonymous]
        [HttpGet("academygroup/{id:guid}")]
        public Task<AcademyGroup?> GetAcademyGroup(Guid id)
        {
            return _dbContext.AcademyGroup.FirstOrDefaultAsync(a => a.Id == id);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<IActionResult> AddUniversity(University university)
        {
            await _dbContext.Universities.AddAsync(university);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPut]
        public async Task<IActionResult> UpdateUniversity(University university)
        {
            _dbContext.Universities.Update(university);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}