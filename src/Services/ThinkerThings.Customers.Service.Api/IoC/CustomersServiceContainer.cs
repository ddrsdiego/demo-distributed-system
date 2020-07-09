namespace ThinkerThings.Customers.Service.IoC
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using ThinkerThings.BuildingBlocks.Cache.Memcached;

    public static class CustomersServiceContainer
    {
        public static IServiceCollection AddCustomersServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHandlers();
            services.AddMassTransit();
            services.AddRespositories();
            services.AddOptions(configuration);
            services.AddMemcached(configuration);
            services.AddSwagger();

            return services;
        }
    }
}