using IotRemoteLaboratory.Mqtt.Core;

namespace IotRemoteLab.API.CLI.Commands
{
    public class StandCommand : ICommand
    {
        private readonly MqttPublisher _mqttPublisher;

        private const string _name = "stand";
        private const string _help = @"
Usage stand [OPTIONS...]
  light       Turn light on if value [1-100] or turn off if 0.
";
        public string Name { get; }
        public string Description { get; }
        public string Help { get; }


        public StandCommand(MqttPublisher mqttPublisher)
        {
            Name = _name;
            Help = _help;
            Description = $"Command used to manipulation stand";
            _mqttPublisher = mqttPublisher;
        }

        // light [OPTIONS...]

        public void Execute(string[] args)
        {
            // command pattern
            // /stand [OPTIONS] standId
            // -1     0         n - last item
            if (args == null || args.Length == 0)
                return;

            if (args[0].Contains("help"))
            {
                Console.WriteLine(Help);
                return;
            }

            switch (args[0])
            {
                case "light": LightHandler(args.Skip(1).ToArray()); break;
                case "webcamera": WebcameraHander(args.Skip(1).ToArray()); break;
                default: Console.WriteLine(Help); break;
            }
        }

        private void WebcameraHander(string[] args) 
        {
            var help = @"
Usage stand webcamera [OPTIONS...];
  OPTIONS:
    [true/false]   State - boolean true/false enable/disable.
";

            if (args == null || args.Length == 0)
            {
                Console.WriteLine("[Error] wrong format use stand webcamera --help");
                return;
            }

            if (args[0].Contains("help")) 
            {
                Console.WriteLine(help);
                return;
            }

            string? payload = args[0] switch
            {
                "0" => "0",
                "1" => "1",
                "true" => "1",
                "false" => "1",
                _ => null
            };
            
            if (payload == null)
            {
                Console.WriteLine(help);
                return;
            }

            _mqttPublisher.PublishMessageAsync(Topics.Webcamera.Replace("+", args[^1]), payload);
            //...
        }

        private void LightHandler(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("[Error] wrong format use stand light --help");
                return;
            }

            if (args[0].Contains("help"))
            {
                Console.WriteLine(@"
Usage stand light [OPTIONS...];
  OPTIONS:
    [0-100]   Brightness - integer value from 0 to 100.
");
                return;
            }

            if (!int.TryParse(args[0], out var res))
            {
                Console.WriteLine("[Error] wrong format value must be int [0-100]");
                return;
            }

            if (res < 0 || res > 100)
                Console.WriteLine("[Error] value must be int [0-100]");

            var topic = Topics.LightingBrightness.Replace("+", args[^1]);
            var r = res.ToString();
            Console.WriteLine($"{topic} --- {r}");
            _mqttPublisher.PublishMessageAsync(topic, r);
            /// res...;
        }
    }
}
