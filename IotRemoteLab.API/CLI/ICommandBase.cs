namespace IotRemoteLab.API.CLI
{
    public interface ICommand
    {
        public string Name { get; }
        public string Description { get; }
        public string Help { get; }
        public void Execute(string[] args);
    }
}
