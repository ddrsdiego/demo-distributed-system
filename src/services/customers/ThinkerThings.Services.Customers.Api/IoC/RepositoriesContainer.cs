using Microsoft.Extensions.DependencyInjection;
using ThinkerThings.Services.Customers.Domain.AggregateModels.CustomerAggregate;
using ThinkerThings.Services.Customers.Infra.Repositories;

namespace ThinkerThings.Services.Customers.IoC
{
    internal static class RepositoriesContainer
    {
        public static IServiceCollection AddRespositories(this IServiceCollection services)
        {
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            return services;
        }
    }
}