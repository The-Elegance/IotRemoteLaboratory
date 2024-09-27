using IotRemoteLab.Domain.Stand.Benchboards;
using System.Text.Json.Serialization;

namespace IotRemoteLab.Domain.Stand
{
    public class Stand
    {
        public Guid Id { get; init; }
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
        public Benchboard? Benchboard { get; set; }
        /// <summary>
        /// Наличие подсветки стенда.
        /// </summary>
        public bool HasLighting { get; set; }
        /// <summary>
        /// Яркость подсветки, значения 0-100.
        /// </summary>
        public uint LightingBrightnessLevel { get; set; }
        /// <summary>
        /// Порт подсветки на Raspberry Pi. 
        /// </summary>
        public uint LightingRaspberryPiPort { get; set; }
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
        /// <summary>
        /// Id для редактора кода
        /// </summary>
        public Guid CodeEditorId { get; set; }
    }


    public static class StandCreator 
    {
        public static Stand Get(Guid id) 
        {
            var stand = new Stand()
            {
                Id = id,
                Mcu = new Mcu()
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
                },
                Url = "stand1.iot-remotelaboratory.local",
                HasBenchboard = true,
                Benchboard = new Benchboard()
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
                            McuPort = "PC_7"
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
                },
                HasLighting = true,
                LigthingBrightnessLevel = 50,
                LigthingRaspberryPiPort = 4,
                HasSerialPort = true,
                SerialPortSpeed = 5,
                HasWebcam = true,
                WebcamUrl = "https://night-world.org/users/_Hel2x_",
                AvailableUarts = [
                    new Uart(Guid.Parse("d9384221-eb57-4f6d-ab73-ae56dcd5d990"), 1, "UART 1.1"),
                    new Uart(Guid.Parse("840b7499-f1ed-48a1-aa91-a1656aa5cbc6"), 2, "UART 1.2"),
                    new Uart(Guid.Parse("69e44773-3cea-4dfc-bd8a-28f697e83e6c"), 3, "UART 1.3"),
                    new Uart(Guid.Parse("21190ead-52cf-4a73-9269-5158a4fdba5b"), 4, "UART 1.4"),
                ],
                CodeEditorId = Guid.Parse("bf40a9e4-eabd-4b62-94b3-dc273328684f")
            };

            return stand;
        }
    }
}
