using Asp.Versioning;
using IotRemoteLab.Domain.Code;
using IotRemoteLab.Domain.Role;
using IotRemoteLab.Domain.Stand;
using IotRemoteLab.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IotRemoteLab.API.Controllers
{
    [Authorize]
    [ApiVersion(1.0)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class StandsController : ControllerBase
    {
        private ApplicationContext _context;

        public StandsController(ApplicationContext context)
        {
            _context = context;
        }


        #region Crud Methods

        [HttpGet]
        public async Task<IEnumerable<Stand>> GetStands()
        {
            return await _context.Stands
                .Include(x => x.Mcu)
                    .ThenInclude(x => x.Framework)
                .Include(x => x.Benchboard)
                    .ThenInclude(x => x.Ports)
                .Include(x => x.AvailableUarts)
                .ToListAsync();
        }

        [HttpGet("{id:long}")]
        [AllowAnonymous]
        public async Task<Stand?> GetStand(long id)
        {
            return await _context.Stands
                .Include(x => x.Mcu)
                    .ThenInclude(x => x.Framework)
                .Include(x => x.Benchboard)
                    .ThenInclude(x => x.Ports)
                .Include(x => x.AvailableUarts)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Add(Stand stand)
        {
            _context.Add(stand);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Update(Stand stand)
        {
            _context.Update(stand);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Delete(Guid id)
        {
            return NotFound();
        }


        #endregion Methods


        #region Current Stand data

        /// TODO !!! использовать данные из БД, вместо статичных конструкций

        private static readonly BoilerplateCode _cppBoilerplateCode = new("cpp", "17", "void main() {\n}");

        [HttpGet("{standId}/defaultBoilderplaceCode")]
        public async Task<ActionResult<BoilerplateCode>> GetDefaultProgrammingPattern(Guid standId)
        {
            return Ok(_cppBoilerplateCode);
        }

        #endregion
    }
}
