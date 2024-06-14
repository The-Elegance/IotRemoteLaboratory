using IotRemoteLab.Domain.Code;
using IotRemoteLab.Domain.Role;
using IotRemoteLab.Domain.Stand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IotRemoteLab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StandsController : ControllerBase
    {
        private Random random = new();


        //IHubContext<StandHub> _hubContext;
        //public StandsController(IHubContext<StandHub> hubContext)
        //{
        //    _hubContext = hubContext;
        //}

        [HttpGet]
        public async Task<IActionResult> GetStands()
        {
            //await _hubContext.Clients.All.SendAsync("RaspberryPiInPortChange", Guid.NewGuid(), random.Next(0,5));
            return Ok();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Stand>> GetStand(Guid id)
        {
            return Ok(StandCreator.Get(id));
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Add(Stand stand)
        {
            return Ok("stand add");
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


        #region Current Stand data

        /// TODO !!! ������������ ������ �� ��, ������ ��������� �����������

        private static readonly List<Uart> _availableUarts =
        [
            new Uart(Guid.NewGuid(), 1, "UART 1.1"),
            new Uart(Guid.NewGuid(), 2, "UART 1.2"),
            new Uart(Guid.NewGuid(), 3, "UART 1.3"),
            new Uart(Guid.NewGuid(), 4, "UART 1.4"),
        ];


        private static readonly BoilerplateCode _cppBoilerplateCode = new("cpp", "17", "void main() {\n}");


        /// <summary>
        /// ���������� ������ ��������� Uart.
        /// </summary>
        /// <param name="standId">Id ������ ��� �������� ��������� �������� ����������</param>
        /// <returns></returns>
        [HttpGet("{standId}/availableUarts")]
        public async Task<ActionResult<List<Uart>>> GetAvailableUartsList(Guid standId)
        {
            return Ok(_availableUarts);
        }


        [HttpGet("{standId}/defaultBoilderplaceCode")]
        public async Task<ActionResult<BoilerplateCode>> GetDefaultProgrammingPattern(Guid standId) 
        {
            return Ok(_cppBoilerplateCode);
        }

        #endregion
    }
}
