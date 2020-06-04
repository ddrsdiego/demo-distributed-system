using Microsoft.Extensions.DependencyInjection;
using MediatR;
using ThinkerThings.Service.CustomerManagement.Application.Commands.RegisterNewCustomer;

namespace ThinkerThings.Service.CustomerManagement.IoC
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