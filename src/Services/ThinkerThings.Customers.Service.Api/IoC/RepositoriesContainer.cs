using Microsoft.Extensions.DependencyInjection;
using ThinkerThings.Customers.Service.Domain.AggregateModels.CustomerAggregate;
using ThinkerThings.Customers.Service.Infra.Repositories;

namespace ThinkerThings.Customers.Service.IoC
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