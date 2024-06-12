using IotRemoteLab.Domain.Stand.Benchboards;
using Microsoft.AspNetCore.Mvc;

namespace IotRemoteLab.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BenchboardsController : ControllerBase
    {
        public static List<Benchboard> benchList = new()
        {
            new Benchboard()
            {
                Id = Guid.Parse("7675c7f4-5f69-4e50-bcca-5c0c14babf4b"),
                Name = "Basic stand STM32 Adapter Board",
                Ports =
                    [
                        new BenchboardPort()
                        {
                            Id = Guid.Parse("b8a8004f-8eb2-4cf5-b6d4-10afcb2e0320"),
                            Type = BenchboardPortType.Input,
                            RaspberryPiPort = 22,
                            McuPort = "PA_5"
                        },
                        new BenchboardPort()
                        {
                            Id = Guid.Parse("5d5ed6c8-f55b-47b3-8e9a-0c50fa97a9ec"),
                            Type = BenchboardPortType.Input,
                            RaspberryPiPort = 23,
                            McuPort = "PA_6"
                        },
                        new BenchboardPort()
                        {
                            Id = Guid.Parse("ae398476-31ae-4db0-9234-93f62a27e2b0"),
                            Type = BenchboardPortType.Input,
                            RaspberryPiPort = 24,
                            McuPort = "PA_7"
                        },
                        new BenchboardPort()
                        {
                            Id = Guid.Parse("a021d31c-4213-4ff0-a65b-127967e0e936"),
                            Type = BenchboardPortType.Input,
                            RaspberryPiPort = 12,
                            McuPort = "PC_5"
                        },
                        new BenchboardPort()
                        {
                            Id = Guid.Parse("f1cdc5c4-9757-4035-b43f-972bb7be2155"),
                            Type = BenchboardPortType.Input,
                            RaspberryPiPort = 13,
                            McuPort = "PC_6"
                        },
                        new BenchboardPort()
                        {
                            Id = Guid.Parse("3bc7236e-0151-4492-9312-729e4bd715fb"),
                            Type = BenchboardPortType.Input,
                            RaspberryPiPort = 16,
                            McuPort = "PC_6"
                        },

                        new BenchboardPort()
                        {
                            Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                            Type = BenchboardPortType.Output,
                            RaspberryPiPort = 19,
                            McuPort = "PA_13"
                        },
                        new BenchboardPort()
                        {
                            Id = Guid.Parse("9f7428d7-9914-4dfd-bbf4-5fcca7871ee7"),
                            Type = BenchboardPortType.Output,
                            RaspberryPiPort = 20,
                            McuPort = "PA_14"
                        },
                        new BenchboardPort()
                        {
                            Id = Guid.Parse("04c5ccf8-aaad-443f-b907-e6f45a3f6ea5"),
                            Type = BenchboardPortType.Output,
                            RaspberryPiPort = 21,
                            McuPort = "PA_15"
                        },
                        new BenchboardPort()
                        {
                            Id = Guid.Parse("b6ebbe90-93f4-4c48-9276-3ea638fa4337"),
                            Type = BenchboardPortType.Output,
                            RaspberryPiPort = 25,
                            McuPort = "PC_13"
                        },
                        new BenchboardPort()
                        {
                            Id = Guid.Parse("e42a8f62-2f39-4895-b3ab-b3da688c820e"),
                            Type = BenchboardPortType.Output,
                            RaspberryPiPort = 26,
                            McuPort = "PC_14"
                        },
                        new BenchboardPort()
                        {
                            Id = Guid.Parse("a8882540-ddb5-44e1-9eb9-410c9896aa0d"),
                            Type = BenchboardPortType.Output,
                            RaspberryPiPort = 27,
                            McuPort = "PC_15"
                        },
                    ]
            }
        };

        public static List<BenchboardPort> portList = new()
        {
            new BenchboardPort()
            {
                Id = Guid.Parse("b8a8004f-8eb2-4cf5-b6d4-10afcb2e0320"),
                Type = BenchboardPortType.Input,
                RaspberryPiPort = 22,
                McuPort = "PA_5"
            }
        };

        #region Benchboard


        [HttpGet]
        public async Task<IActionResult> GetBenchboards()
        {
            return Ok(benchList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBenchboard(Guid id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult AddBenchboard(Benchboard bench)
        {
            if (bench == null)
                Console.WriteLine("bench is null");
            else
                benchList.Add(bench);

            return Ok("bench add");
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
            return Ok(portList);
        }

        [HttpGet("ports/{id}")]
        public async Task<IActionResult> GetBenchboardPort(Guid id)
        {
            return Ok();
        }

        [HttpPost("ports")]
        public IActionResult AddBenchboardPort(BenchboardPort port)
        {
            if (port == null)
                Console.WriteLine("port is null");
            else
                portList.Add(port);

            return Ok("port add");
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
