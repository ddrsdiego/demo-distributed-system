using Enyim.Caching.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace ThinkerThings.BuildingBlocks.Cache.Memcached
{
    public static class MemcachedServiceCollectionExtensions
    {
        public static IServiceCollection AddMemcached(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            services.AddEnyimMemcached(o => o.Servers = new List<Server>
            {
                CreateServerConfig(configuration)
            });

            services.AddSingleton<ICacheProvider, CacheProvider>();
            services.AddSingleton<ICacheRepository, CacheRepository>();

            return services;
        }

        private static Server CreateServerConfig(IConfiguration configuration)
        {
            const int PORT_DEFAULT = 11211;
            const string ADDRESS_DEFAULT = "localhost";

            var serverPort = configuration["MemcachedSettings:Port"];
            var serverAddress = configuration["MemcachedSettings:Address"];

            if (string.IsNullOrEmpty(serverPort) || string.IsNullOrEmpty(serverAddress))
                return new Server { Address = ADDRESS_DEFAULT, Port = PORT_DEFAULT };

            return new Server { Address = serverAddress, Port = int.Parse(serverPort) };
        }
    }
}