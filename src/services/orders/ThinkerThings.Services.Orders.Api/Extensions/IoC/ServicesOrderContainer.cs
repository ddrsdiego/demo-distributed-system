using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ThinkerThings.Services.Orders.Extensions.IoC
{
    public static class ServicesOrderContainer
    {
        public static IServiceCollection AddOrderServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions(configuration);
            services.AddMassTransitConsumers();

            return services;
        }
    }
}