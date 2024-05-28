using IotRemoteLab.Blazor.Commands;

namespace IotRemoteLab.Blazor.Commands.Stand
{
    public sealed class StandCommand : CommandBase
    {
        const string CommandHeader = "stand";


        private readonly Action<string> _commandExecuteResultMessage;


        public StandCommand(Action<string> commandExecuteResultMessage) : base(CommandHeader, "")
        {
            _commandExecuteResultMessage = commandExecuteResultMessage;
        }

        public override void Execute(string[] args)
        {
            if (args == null || args.Length == 0)
                return;

            switch (args.Length)
            {
                case 2:
                    {
                        switch (args[1])
                        {
                            case "led":
                                Led(int.Parse(args[1]));
                                break;
                            case "message":
                                Message(args[1]);
                                break;
                            case "webcamera":
                                Webcamera(bool.Parse(args[1]));
                                break;
                        }
                    }
                    break;
                case 4:
                    {
                        switch (args[1])
                        {
                            case "gpio":
                                switch (args[2])
                                {
                                    case "button":
                                        GpioButton(bool.Parse(args[3]));
                                        break;
                                }
                                break;
                        }
                    }
                    break;
            }
            //""
            // /stand led 50
            // /stand message "Test Message"
            // /stand webcamera true
            // /stand enable led true

            // /stand gpio button true
        }

        private void Led(int value)
        {

        }

        private void Message(string value)
        {

        }

        private void Webcamera(bool state)
        {

        }

        private void GpioButton(bool state)
        {

        }
    }
}
