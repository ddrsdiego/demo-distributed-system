using Microsoft.Extensions.DependencyInjection;
using ThinkerThings.Service.CustomerManagement.Domain.AggregateModels.CustomerAggregate;
using ThinkerThings.Service.CustomerManagement.Infra.Repositories;

namespace ThinkerThings.Service.CustomerManagement.IoC
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