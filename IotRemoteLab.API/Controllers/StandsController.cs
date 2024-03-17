using Microsoft.AspNetCore.Mvc;

namespace IotRemoteLab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StandsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetStands()
        {
            return Ok();
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetStand(Guid id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Add(object stand)
        {
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(object stand)
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            return Ok();
        }
    }
}
