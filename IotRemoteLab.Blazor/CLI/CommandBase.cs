namespace IotRemoteLab.Blazor.Commands
{
    public abstract class CommandBase
    {
        public string Name { get; }
        public string Help { get; }


        protected CommandBase(string name, string help)
        {
            Name = name;
            Help = help;
        }


        public abstract void Execute(string[] args);
    }
}
