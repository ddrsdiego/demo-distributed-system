using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ThinkerThings.Services.Customers.Infra.Options;

namespace ThinkerThings.Services.Customers.IoC
{
    internal static class MassTransitContainer
    {
        public static IServiceCollection AddMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(configure =>
            {
                configure.AddBus(serviceProvider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var rabbitMQSettings = serviceProvider.GetRequiredService<IOptions<RabbitMQConnectionSetting>>().Value;

                    cfg.Host($"rabbitmq://{rabbitMQSettings.Host}", h =>
                    {
                        h.Username(rabbitMQSettings.UserName);
                        h.Password(rabbitMQSettings.Password);
                    });
                }));
            });

            return services;
        }
    }
}