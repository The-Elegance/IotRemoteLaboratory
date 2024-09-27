using IotRemoteLab.Domain.Code;
using IotRemoteLab.Domain.Role;
using IotRemoteLab.Domain.Stand;
using IotRemoteLab.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IotRemoteLab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StandsController : ControllerBase
    {
        private Random random = new();
        private ApplicationContext _context;


        public StandsController(ApplicationContext context)
        {
            _context = context;
        }


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
        public async Task<Stand?> GetStand(long id)
        {
            return await _context.Stands.
                Include(x => x.Mcu).
                ThenInclude(x => x.Framework).
                Include(x => x.Benchboard)
                .ThenInclude(x => x.Ports).
                FirstOrDefaultAsync(i => i.Id == id);
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

        /// TODO !!! использовать данные из БД, вместо статичных конструкций

        private static readonly List<Uart> _availableUarts =
        [
            new Uart(0, 1, "UART 1.1"),
            new Uart(1, 2, "UART 1.2"),
            new Uart(2, 3, "UART 1.3"),
            new Uart(3, 4, "UART 1.4"),
        ];


        private static readonly BoilerplateCode _cppBoilerplateCode = new("cpp", "17", "void main() {\n}");


        /// <summary>
        /// Возвращает список доступных Uart.
        /// </summary>
        /// <param name="standId">Id стенда для которого требуется получить информацию</param>
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
