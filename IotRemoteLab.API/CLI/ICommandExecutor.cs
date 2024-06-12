namespace IotRemoteLab.API.CLI
{
    public interface ICommandExecutor
    {
        public string[] AvailableCommands { get; }
        ICommand? FindCommandByName(string commandName);
        void Execute(string[] args);
    }
}
