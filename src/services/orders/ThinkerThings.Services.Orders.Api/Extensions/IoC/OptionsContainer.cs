using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ThinkerThings.Services.Orders.Infra.Options;

namespace ThinkerThings.Services.Orders.Extensions.IoC
{
    public static class OptionsContainer
    {
        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMQConnectionSetting>(configuration.GetSection(nameof(RabbitMQConnectionSetting)));

            return services;
        }
    }
}