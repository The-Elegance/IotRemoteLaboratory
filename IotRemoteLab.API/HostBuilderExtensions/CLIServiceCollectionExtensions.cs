using IotRemoteLab.API.CLI;
using IotRemoteLab.API.CLI.Commands;
using System.Reflection;

namespace IotRemoteLab.API.HostBuilderExtensions
{
    public static class CLIServiceCollectionExtensions
    {
        public static IServiceCollection AddCLI(this IServiceCollection services)
        {
            var list = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => typeof(ICommand).IsAssignableFrom(t));

            foreach (var item in list.Skip(1))
            {
                services.AddSingleton(list.FirstOrDefault(), item);
            }

            services.AddSingleton<ICommand, StandCommand>();
            services.AddSingleton<ICommandExecutor, CommandExecutor>(_ => new CommandExecutor(_.GetServices<ICommand>().ToArray()));
            return services;
        }
    }
}
