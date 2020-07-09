using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ThinkerThings.Services.Customers.Application.Commands;

namespace ThinkerThings.Services.Customers.IoC
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