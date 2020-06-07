# Demo Distributed System

Esse projeto tem o intuito de demonstrar como sistemas distribuídos podem transmitir mensagens entre si, utilizar mais de um mecanismo de persistência entre outras tecnologias o projeto for sendo incrementado.

**WARNING**

Nesse primeiro momento a aplicação que faz uso persistência dados é `ThinkerThings.Customers.Service.Api`.
Os próximos passos desse repositório será implementar a persistência dos dados recebidos no `ThinkerThings.Orders.Service.Api` e utilizando algum banco de dados NoSQL.

## Imagens Docker
Para o exemplo fora usadas as seguintes imagens:

Para o banco de dados, foi utilizado o MariaDB

`docker run --name mariadb -e MYSQL_ROOT_PASSWORD=SUA_SENHA -d -p 3306:3306 mariadb`

Para o mecanismo de transporte de mensagens, foi utilizad o RabbitMQ.

`docker run --name rabbitmq -d -p 5672:5672 -p 15672:15672 --hostname rabbitmq rabbitmq:3-management`

Para o mecanismo de cache, foi utilizado Memcached

`docker run -p 11211:11211 -d memcached`

## Packages
As aplicações fazem uso dos seguintes packages

https://www.nuget.org/packages/EnyimMemcachedCore/

https://www.nuget.org/packages/MySqlConnector/

https://www.nuget.org/packages/MassTransit.AspNetCore/

https://www.nuget.org/packages/MassTransit.RabbitMQ/

https://www.nuget.org/packages/MassTransit.Extensions.DependencyInjection/

### Rodar as aplicaçãos

A solução contem dois projetos, um para cadastro de manutenção de clientes e outro para requisição de ordens de compra

Nesse primeiro commit do projeto, apenas o cadastro de clientes está “completo”, persistindo dados numa base MariaDB, utilizando cache para consulta de dados e envio de mensagens utilizando o RabbitMQ.

O primeiro passo para rodar a aplicação é baixar as imagens e rodar os containers descritos acima.
Na pasta Scripts do projeto `ThinkerThings.Customers.Service.Api` contem os arquivos para criação das tabelas de clientes.

## Fontes de referência
https://www.twitch.tv/phatboyg

Canal do Twitch do criador do Masstransit

https://jimmybogard.com/

Criador dos pacotes MediatR e AutoMapper além de ótimas apresensetações no NDC.

https://www.stevejgordon.co.uk/

Blog voltado a performance de aplicações ASP.NET Core

https://dotnet.microsoft.com/learn/dotnet/architecture-guides

Ótimos exemplos de como criar a realizar deploy de aplicação com docker e desenvolvimento de microsserviços

https://d0.awsstatic.com/whitepapers/performance-at-scale-with-amazon-elasticache.pdf

Documetanção da Amazon para uso de estratégias de cache

https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md#3-introduction

Guidelines para criação de aplicações REST.