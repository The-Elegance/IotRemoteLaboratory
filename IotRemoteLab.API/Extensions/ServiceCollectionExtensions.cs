using IotRemoteLab.API.Controllers;
using IotRemoteLab.API.Services;

namespace IotRemoteLab.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<AuthService>();
            services.AddScoped<UserService>();
            services.AddScoped<IJwtService, JwtService>();

            services.AddSingleton<IUpperIotService, UpperNodeRedService>();
            services.AddSingleton<StandsService>();

            return services;
        }
    }
}
