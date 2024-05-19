using IotRemoteLab.Domain.Stand;
using IotRemoteLab.Domain.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IotRemoteLab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StandsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetStands()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStand(Guid id)
        {
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Add(Stand stand)
        {
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(Stand stand)
        {
            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Delete(Guid id)
        {
            return Ok();
        }
    }
}
