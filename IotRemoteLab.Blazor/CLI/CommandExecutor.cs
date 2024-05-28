using IotRemoteLab.Blazor.Commands;

namespace IotRemoteLab.Blazor.CLI
{
    public class CommandExecutor
    {
        private readonly CommandRegistry _registry;
        private readonly Action<string> _commandExecuteResultMessage;

        public CommandExecutor(CommandRegistry commandRegistry)
        {
            _registry = commandRegistry;
        }

        public void ExecuteByCommandName(string name) 
        {
            _registry.FindCommandByName(name);
        }

        public void Execute(string[] args)
        {
            if (args[0].Length == 0)
            {
                return;
            }

            var commandName = args[0];
            var cmd = _registry.FindCommandByName(commandName);

            if (cmd == null)
            {
                _commandExecuteResultMessage($"Извините, команда {commandName} не найдена!");
            }
            else 
            {
                cmd.Execute(args.Skip(1).ToArray());
            }
        }
    }
}
