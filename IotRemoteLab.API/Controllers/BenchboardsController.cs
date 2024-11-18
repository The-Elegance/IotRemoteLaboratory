using IotRemoteLab.Domain.Stand;
using IotRemoteLab.Domain.Stand.Benchboards;
using IotRemoteLab.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IotRemoteLab.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BenchboardsController : ControllerBase
    {
        private ApplicationContext _context;

        public BenchboardsController(ApplicationContext context)
        {
            _context = context;
        }

        #region Benchboard


        [HttpGet]
        public async Task<IEnumerable<Benchboard>> GetBenchboards()
        {
            return await _context.Benchboards
                .Include(b => b.Ports)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBenchboard(Guid id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult AddBenchboard(Benchboard benchboard)
        {
            _context.Add(benchboard);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateBenchboard(Benchboard benchboard)
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteBenchboard(Guid id)
        {
            return Ok();
        }


        #endregion Benchboard


        #region Benchboard Port


        [HttpGet("ports")]
        public async Task<IEnumerable<BenchboardPort>> GetBenchboardPorts()
        {
            return await _context.BenchboardPort.ToListAsync();
        }

        [HttpGet("ports/{id}")]
        public async Task<IActionResult> GetBenchboardPort(Guid id)
        {
            return Ok();
        }

        [HttpPost("ports")]
        public IActionResult AddBenchboardPort(BenchboardPort port)
        {
            _context.Add(port);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut("ports")]
        public IActionResult UpdateBenchboardPort(BenchboardPort port)
        {
            return Ok();
        }

        [HttpDelete("ports")]
        public IActionResult DeleteBenchboardPort(Guid id)
        {
            return Ok();
        }


        #endregion Benchboard Port
    }
}
