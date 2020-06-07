using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ThinkerThings.Orders.Service.Extensions.IoC
{
    public static class ServiceOrdersContainer
    {
        public static IServiceCollection AddOrdersServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions(configuration);
            services.AddMassTransitConsumers();

            return services;
        }
    }
}