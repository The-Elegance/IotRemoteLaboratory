using IotRemoteLab.API.Repositories;

namespace IotRemoteLab.API.Extensions
{
    public static class RepositoriesCollectionExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<UsersRepository>();
            services.AddScoped<RolesRepository>();
            services.AddScoped<ScheduleRepository>();
            return services;
        }
    }
}
