using Asp.Versioning;
using IotRemoteLab.Domain.Role;
using IotRemoteLab.Domain.Stand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IotRemoteLab.API.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    [ApiVersion(1.0)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class McuController : ControllerBase
    {
        #region Mcu


        [HttpGet]
        public async Task<IActionResult> GetMcuList()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMcu(Guid id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult AddBenchboard(Mcu mcu)
        {
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
        public async Task<IActionResult> GetMcuFrameworks()
        {
            return Ok();
        }

        [HttpGet("frameworks/{id}")]
        public async Task<IActionResult> GetMcuFramework(Guid id)
        {
            return Ok();
        }

        [HttpPost("frameworks")]
        public IActionResult AddMcuFramework(Mcu mcu)
        {
            return Ok();
        }

        [HttpPut("frameworks")]
        public IActionResult UpdateMcuFramework(Mcu stand)
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
