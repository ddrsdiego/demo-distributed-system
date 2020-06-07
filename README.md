# Demo Distributed System

Esse projeto tem o intuito de demonstrar sistemas distribidos 

## O que é o Memcached
O Memcached é um banco de dados chave e valor de código aberto e alto desempenho, de memória distribuída, destinado a acelerar aplicações Web, aliviando a carga do banco de dados.

Para mais informações consultar o site: https://memcached.org/

## Docker
Para o exemplo foram usadas as seguintes imagens:

`docker run --name mariadb -e MYSQL_ROOT_PASSWORD=SUA_SENHA -d -p 3306:3306 mariadb`

`docker run --name rabbitmq -d -p 5672:5672 -p 15672:15672 --hostname rabbitmq rabbitmq:3-management`

`docker run -p 11211:11211 -d memcached`

## Packages
A aplicação faz uso do NuGet package `EnyimMemcachedCore`
https://www.nuget.org/packages/EnyimMemcachedCore/


### Rodar a aplicação

**WARNING**
Caso não seja definido a configuração abaixo no appSettings, a aplicação irá ser configurada para rodar localmente.

```json
  "MemcachedSettings": {
    "Port": 11211,
    "Address": "localhost"
  }
```


```csharp
    public static class MemcachedConfigurator
    {
        public static IServiceCollection AddMemcached(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEnyimMemcached(o => o.Servers = new List<Server>
            {
                CreateServerConfig(services)
            });

            services.AddSingleton<ICacheProvider, CacheProvider>();
            services.AddSingleton<ICacheRepository, CacheRepository>();

            return services;
        }

        private static Server CreateServerConfig(IServiceCollection services)
        {
            const int PORT_DEFAULT = 11211;
            const string ADDRESS_DEFAULT = "localhost";

            var sp = services.BuildServiceProvider();
            IOptions<MemcachedSettings> memcachedSettings = sp.GetService<IOptions<MemcachedSettings>>();

            if (memcachedSettings.Value is null)
                return new Server { Address = ADDRESS_DEFAULT, Port = PORT_DEFAULT };

            return new Server { Address = memcachedSettings.Value.Address, Port = memcachedSettings.Value.Port };
        }
    }
```
