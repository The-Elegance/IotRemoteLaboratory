using IotRemoteLab.Domain.Stand.Benchboards;
using System.Text.Json.Serialization;

namespace IotRemoteLab.Domain.Stand
{
    public class Stand
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Микроконтроллер.
        /// </summary>
        public Mcu Mcu { get; set; }
        /// <summary>
        /// Фреймворк микроконтроллера.
        /// </summary>
        [JsonIgnore]
        public McuFramework Framework { get => Mcu.Framework; }
        /// <summary>
        /// Url (Доменное имя)
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Наличие стендовой платы
        /// </summary>
        public bool HasBenchboard { get; set; }
        /// <summary>
        /// Стендовая плата, если HasBenchboard = false, то значением будет null.
        /// </summary>
        public Benchboard Benchboard { get; set; }
        /// <summary>
        /// Наличие подсветки стенда.
        /// </summary>
        public bool HasLighting { get; set; }
        /// <summary>
        /// Яркость подсветки, значения 0-100.
        /// </summary>
        public uint LigthingBrightnessLevel { get; set; }
        /// <summary>
        /// Порт подсветки на Raspberry Pi. 
        /// </summary>
        public uint LigthingRaspberryPiPort { get; set; }
        /// <summary>
        /// Наличие последовательного порта.
        /// </summary>
        public bool HasSerialPort { get; set; }
        /// <summary>
        /// Скорость последовательного порта.
        /// </summary>
        public uint SerialPortSpeed { get; set; }
        /// <summary>
        /// Наличие вебкамеры.
        /// </summary>
        public bool HasWebcam { get; set; }
        /// <summary>
        /// Ссылка на web трансляцию.
        /// </summary>
        public string WebcamUrl { get; set;}
        /// <summary>
        /// Доступные Uarts для стенда.
        /// </summary>
        public List<Uart> AvailableUarts { get; set; } 
    }


    public static class StandCreator 
    {
        public static Stand Get(Guid id) 
        {
            var stand = new Stand()
            {
                Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                Mcu = new Mcu()
                {
                    Id = Guid.Parse("25638a09-f591-488b-bfe4-0ee680a8ade7"),
                    Framework = new McuFramework()
                    {
                        Id = Guid.Parse("60f8bc4d-aa37-4457-95d9-685a89c1b849"),
                        Name = "OpenGL",
                        Pattern = "void main() { glInit(0, 1); }",
                    },
                    AssemblyScriptFile = "assembly.yml",
                    DeployScriptFile = "deploy.yml"
                },
                Url = "night-world.org",
                HasBenchboard = true,
                Benchboard = new Benchboard()
                {
                    Id = Guid.Parse("7675c7f4-5f69-4e50-bcca-5c0c14babf4b"),
                    Name = "Stand 1",
                    Ports =
                    [
                        new BenchboardPort()
                        {
                            Id = Guid.Parse("b8a8004f-8eb2-4cf5-b6d4-10afcb2e0320"),
                            Type = BenchboardPortType.Input,
                            RaspberryPiPort = 0,
                            McuPort = "IN_PA_5"
                        },
                        new BenchboardPort()
                        {
                            Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                            Type = BenchboardPortType.Output,
                            RaspberryPiPort = 1,
                            McuPort = "OUT_PA_2"
                        }
                    ]
                },
                HasLighting = true,
                LigthingBrightnessLevel = 0,
                HasSerialPort = true,
                SerialPortSpeed = 5,
                HasWebcam = true,
                WebcamUrl = "https://night-world.org/users/_Hel2x_",
                AvailableUarts = [
                    new Uart(Guid.Parse("d9384221-eb57-4f6d-ab73-ae56dcd5d990"), 1, "UART 1.1"),
                    new Uart(Guid.Parse("840b7499-f1ed-48a1-aa91-a1656aa5cbc6"), 2, "UART 1.2"),
                    new Uart(Guid.Parse("69e44773-3cea-4dfc-bd8a-28f697e83e6c"), 3, "UART 1.3"),
                    new Uart(Guid.Parse("21190ead-52cf-4a73-9269-5158a4fdba5b"), 4, "UART 1.4"),
                ]
            };

            return stand;
        }
    }
}
