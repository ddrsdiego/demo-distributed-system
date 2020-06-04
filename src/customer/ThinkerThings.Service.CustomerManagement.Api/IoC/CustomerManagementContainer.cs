using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ThinkerThings.BuildingBlocks.Cache.Memcached;

namespace ThinkerThings.Service.CustomerManagement.IoC
{
    public static class CustomerManagementContainer
    {
        public static IServiceCollection AddCustomerManagementServices(this IServiceCollection services)
        {
            services.AddHandlers();
            services.AddMemcached();
            services.AddRespositories();

            return services;
        }
    }
}