using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ThinkerThings.Customers.Service.Infra.Options;

namespace ThinkerThings.Customers.Service.IoC
{
    public static class OptionsContainer
    {
        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMQConnectionSetting>(configuration.GetSection(nameof(RabbitMQConnectionSetting)));
            services.Configure<ConnectionStringOptions>(connectionStringOptions => connectionStringOptions.MySqlConnection = configuration.GetConnectionString("MySqlConnection"));

            return services;
        }
    }
}