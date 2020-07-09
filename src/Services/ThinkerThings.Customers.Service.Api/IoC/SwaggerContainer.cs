namespace ThinkerThings.Customers.Service.IoC
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    using System;

    internal static class SwaggerContainer
    {
        private const string REPOSITORIO = "https://github.com/ddrsdiego/demo-distributed-system";

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Sistemas de manuteção de cadastro de clientes",
                    Version = "v1",
                    Description = "API por cotação e conversão de moeda estrangeira para reais.",
                    Contact = new OpenApiContact
                    {
                        Url = new Uri(REPOSITORIO),
                        Name = "Diego Dias Ribeiro da Silva",
                        Email = "ddrsdiego@hotmail.com"
                    }
                });
            });

            return services;
        }
    }
}