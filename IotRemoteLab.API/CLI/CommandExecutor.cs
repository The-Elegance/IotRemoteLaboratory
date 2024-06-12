namespace IotRemoteLab.API.CLI
{
    public class CommandExecutor : ICommandExecutor
    {
        private ICommand[] _commandsList;
        public string[] AvailableCommands { get; }

        public CommandExecutor(ICommand[] commandsList)
        {
            _commandsList = commandsList;
            AvailableCommands = _commandsList.Select(cmd => $"{cmd.Name}     {cmd.Description}").ToArray();
        }

        public ICommand? FindCommandByName(string commandName) 
        {
            commandName = commandName.ToLower().Remove(0, 1);
            return _commandsList.FirstOrDefault(command => string.Equals(command.Name, commandName, StringComparison.OrdinalIgnoreCase));
        }

        public void Execute(string[] args) 
        {
            if (args[0].Length == 0) 
            {
                return;
            }

            var commandName = args[0];
            var cmd = FindCommandByName(commandName);
            if (cmd == null)
                return;

            cmd.Execute(args.Skip(1).ToArray());
        }
    }
}
