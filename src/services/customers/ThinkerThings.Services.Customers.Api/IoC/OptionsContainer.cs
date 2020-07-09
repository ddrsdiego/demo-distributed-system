using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ThinkerThings.Services.Customers.Infra.Options;

namespace ThinkerThings.Services.Customers.IoC
{
    internal static class OptionsContainer
    {
        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMQConnectionSetting>(configuration.GetSection(nameof(RabbitMQConnectionSetting)));
            services.Configure<ConnectionStringOptions>(connectionStringOptions => connectionStringOptions.MySqlConnection = configuration.GetConnectionString("MySqlConnection"));

            return services;
        }
    }
}