using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ThinkerThings.BuildingBlocks.Cache.Memcached;

namespace ThinkerThings.Customers.Service.IoC
{
    public static class CustomersServiceContainer
    {
        public static IServiceCollection AddCustomersServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHandlers();
            services.AddMassTransit();
            services.AddRespositories();
            services.AddOptions(configuration);
            services.AddMemcached(configuration);

            return services;
        }
    }
}