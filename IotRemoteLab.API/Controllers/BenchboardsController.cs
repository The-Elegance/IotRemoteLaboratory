using Asp.Versioning;
using IotRemoteLab.Domain.Role;
using IotRemoteLab.Domain.Stand.Benchboards;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IotRemoteLab.API.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    [ApiVersion(1.0)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BenchboardsController : ControllerBase
    {
        #region Benchboard


        [HttpGet]
        public async Task<IActionResult> GetBenchboards()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBenchboard(Guid id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult AddBenchboard(Benchboard stand)
        {
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateBenchboard(Benchboard stand)
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
        public async Task<IActionResult> GetBenchboardPorts()
        {
            return Ok();
        }

        [HttpGet("ports/{id}")]
        public async Task<IActionResult> GetBenchboardPort(Guid id)
        {
            return Ok();
        }

        [HttpPost("ports")]
        public IActionResult AddBenchboardPort(BenchboardPort port)
        {
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
