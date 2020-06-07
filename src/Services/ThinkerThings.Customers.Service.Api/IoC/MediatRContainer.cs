using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ThinkerThings.Customers.Service.Application.Commands;

namespace ThinkerThings.Customers.Service.IoC
{
    internal static class MediatRContainer
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddMediatR(typeof(RegisterNewCustomerCommand).Assembly);
            return services;
        }
    }
}