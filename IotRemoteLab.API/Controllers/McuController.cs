using IotRemoteLab.Domain.Stand;
using Microsoft.AspNetCore.Mvc;

namespace IotRemoteLab.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class McuController : ControllerBase
    {
        public static List<Mcu> mcuList = new()
        {
            new Mcu()
            {
                Id = Guid.Parse("25638a09-f591-488b-bfe4-0ee680a8ade7"),
                Name = "STM32F401RE",
                Framework = new McuFramework()
                {
                    Id = Guid.Parse("60f8bc4d-aa37-4457-95d9-685a89c1b849"),
                    Name = "Mbed OS 5",
                    Pattern = "#include \"mbed.h\" \r\n    Serial pc(PIN_TX, PIN_RX); // tx, rx\r\n    DigitalOut led(PIN_LED);\r\n    int main() {        \r\n    pc.baud(115200);\r\n    while(1) {\r\n    led = 1;\r\n    wait(1);\r\n    led = 0;\r\n    wait(1);        \r\n    pc.printf(\"Finish a period\\r\\n\"); } }",
                },
                AssemblyScriptFile = "assembly.yml",
                DeployScriptFile = "deploy.yml"
            }
        };

        public static List<McuFramework> framList = new()
        {
            new McuFramework()
            {
                Id = Guid.Parse("60f8bc4d-aa37-4457-95d9-685a89c1b849"),
                Name = "Mbed OS 5",
                Pattern = "#include \"mbed.h\" \r\n    Serial pc(PIN_TX, PIN_RX); // tx, rx\r\n    DigitalOut led(PIN_LED);\r\n    int main() {        \r\n    pc.baud(115200);\r\n    while(1) {\r\n    led = 1;\r\n    wait(1);\r\n    led = 0;\r\n    wait(1);        \r\n    pc.printf(\"Finish a period\\r\\n\"); } }",
            }
        };

        #region Mcu 

        [HttpGet]
        public async Task<IActionResult> GetMcuList()
        {
            return Ok(mcuList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMcu(Guid id)
        {
            return Ok();
        }

        [HttpPost]

        public IActionResult PostMcu(Mcu mcu)
        {
            if (mcu == null)
                Console.WriteLine("mcu is null");
            else
                mcuList.Add(mcu);

            return Ok("mcu add");
        }

        [HttpPut]
        public IActionResult UpdateMcu(Mcu stand)
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteMcu(Guid id)
        {
            return Ok();
        }


        #endregion Mcu


        #region Mcu Framework


        [HttpGet("frameworks")]
        public async Task<IActionResult> GetMcuFrameworks()
        {
            return Ok(framList);
        }

        [HttpGet("frameworks/{id}")]
        public async Task<IActionResult> GetMcuFramework(Guid id)
        {
            return Ok();
        }

        [HttpPost("frameworks")]
        public IActionResult AddMcuFramework(McuFramework fram)
        {
            if (fram == null)
                Console.WriteLine("fram is null");
            else
                framList.Add(fram);

            return Ok("fram add");
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
