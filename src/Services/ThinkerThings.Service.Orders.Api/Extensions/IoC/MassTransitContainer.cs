using GreenPipes;
using MassTransit;
using MassTransit.AspNetCoreIntegration;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using ThinkerThings.Orders.Service.Application.Consumers;
using ThinkerThings.Service.Messages;
using ThinkerThings.Service.Orders.Infra.Options;

namespace ThinkerThings.Orders.Service.Extensions.IoC
{
    internal static class MassTransitContainer
    {
        public static IServiceCollection AddMassTransitConsumers(this IServiceCollection services)
        {
            services.AddMassTransit(ConfigureConsumer, ConfigureMassTransit);
            return services;
        }

        private static IBusControl ConfigureConsumer(IServiceProvider serviceProvider)
        {
            var rabbitMQSettings = serviceProvider.GetRequiredService<IOptions<RabbitMQConnectionSetting>>().Value;

            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host($"rabbitmq://{rabbitMQSettings.Host}", configure =>
                {
                    configure.Username(rabbitMQSettings.UserName);
                    configure.Password(rabbitMQSettings.Password);
                });

                cfg.ReceiveEndpoint($"{typeof(NewCustomerRegistered).Assembly.GetName().Name}-{typeof(NewCustomerRegistered).Name}", endPoint =>
                {
                    endPoint.PrefetchCount = 1;
                    endPoint.UseMessageRetry(r => r.Interval(1, TimeSpan.FromMinutes(1)));
                    endPoint.ConfigureConsumer<NewCustomerRegisteredConsumer>(serviceProvider);
                });

                cfg.ReceiveEndpoint($"{typeof(CustomerDisabled).Assembly.GetName().Name}-{typeof(CustomerDisabled).Name}", endPoint =>
                {
                    endPoint.PrefetchCount = 1;
                    endPoint.UseMessageRetry(r => r.Interval(1, TimeSpan.FromMinutes(1)));
                    endPoint.ConfigureConsumer<CustomerDisabledConsumer>(serviceProvider);
                });
            });
        }

        private static void ConfigureMassTransit(IServiceCollectionConfigurator configurator)
        {
            configurator.AddConsumer<CustomerDisabledConsumer>();
            configurator.AddConsumer<NewCustomerRegisteredConsumer>();
        }
    }
}