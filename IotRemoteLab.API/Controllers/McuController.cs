using IotRemoteLab.Domain.Stand;
using IotRemoteLab.Domain.Stand.Benchboards;
using IotRemoteLab.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IotRemoteLab.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class McuController : ControllerBase
    {
        private ApplicationContext _context;

        public McuController(ApplicationContext context)
        {
            _context = context;
        }
        #region Mcu


        [HttpGet]
        public async Task<IEnumerable<Mcu>> GetMcuList()
        {
            return await _context.Mcus
                .Include(b => b.Framework)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMcu(Guid id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult AddBenchboard(Mcu mcu)
        {
            _context.Add(mcu);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateBenchboard(Mcu stand)
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteBenchboard(Guid id)
        {
            return Ok();
        }


        #endregion Mcu


        #region Mcu Framework


        [HttpGet("frameworks")]
        public async Task<IEnumerable<McuFramework>> GetMcuFrameworks()
        {
            return await _context.McuFrameworks.ToListAsync();
        }

        [HttpGet("frameworks/{id}")]
        public async Task<IActionResult> GetMcuFramework(Guid id)
        {
            return Ok();
        }

        [HttpPost("frameworks")]
        public IActionResult AddMcuFramework(McuFramework mcuFramework)
        {
            _context.Add(mcuFramework);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut("frameworks")]
        public IActionResult UpdateMcuFramework(McuFramework mcuFramework)
        {
            return Ok();
        }

        [HttpDelete("frameworks")]
        public IActionResult DeleteMcuFramework(Guid id)
        {
            return Ok();
        }


        #endregion Mcu Framework
    }
}
