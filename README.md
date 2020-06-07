# Demo Distributed System

Esse projeto tem o intuito de demonstrar como sistemas distribidos podem transmitir mensagens entre si

## Imagens Docker
Para o exemplo foram usadas as seguintes imagens:

Para o banco de dados, foi utilizado o MariaDB
`docker run --name mariadb -e MYSQL_ROOT_PASSWORD=SUA_SENHA -d -p 3306:3306 mariadb`

Para o mecanismo de transporte de mensagens, foi utilizad o RabbitMQ.
`docker run --name rabbitmq -d -p 5672:5672 -p 15672:15672 --hostname rabbitmq rabbitmq:3-management`

Para o mecanismo de cache, foi utilizado Memcached
`docker run -p 11211:11211 -d memcached`

## Packages
As aplicaçções fazem uso dos seguintes packages

https://www.nuget.org/packages/EnyimMemcachedCore/
https://www.nuget.org/packages/MySqlConnector/
https://www.nuget.org/packages/MassTransit.AspNetCore/
https://www.nuget.org/packages/MassTransit.RabbitMQ/
https://www.nuget.org/packages/MassTransit.Extensions.DependencyInjection/

### Rodar as aplicaçãos

**WARNING**
Nesse primeiro momento a aplicação que faz uso persistencia dados é `ThinkerThings.Customers.Service.Api`.
Os próximos passos desse repositório será implementar a persistencia dos dados recebidos no `ThinkerThings.Orders.Service.Api` e utilizando algum banco de dados NoSQL.



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
