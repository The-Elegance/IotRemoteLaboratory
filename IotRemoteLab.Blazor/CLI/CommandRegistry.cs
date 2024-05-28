using IotRemoteLab.Blazor.Commands;

namespace IotRemoteLab.Blazor.CLI
{
    public sealed class CommandRegistry
    {
        private readonly CommandBase[] _commands;

        public CommandRegistry(CommandBase[] commands)
        {
            _commands = commands;
        }

        public string RegistryHelp() 
        {
            return string.Empty;
        }

        public string[] GetAvailableCommandName() 
        {
            return _commands.Select(c => c.Name).ToArray();
        }

        public CommandBase FindCommandByName(string name) 
        {
            return _commands.FirstOrDefault(c => string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
