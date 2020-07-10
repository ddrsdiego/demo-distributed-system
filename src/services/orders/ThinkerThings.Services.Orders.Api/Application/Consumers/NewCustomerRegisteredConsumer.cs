using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using ThinkerThings.Services.Orders.Domain.AggregateModels.CustomerAggregate;
using ThinkerThings.Service.Messages;

namespace ThinkerThings.Services.Orders.Application.Consumers
{
    public class NewCustomerRegisteredConsumer : IConsumer<NewCustomerRegistered>
    {
        private readonly ILogger<NewCustomerRegisteredConsumer> _logger;

        public NewCustomerRegisteredConsumer(ILogger<NewCustomerRegisteredConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<NewCustomerRegistered> context)
        {
            await Task.CompletedTask;

            var customer = new Customer
            {
                CustomerId = context.Message.Id,
                Name = context.Message.Name,
                Address = context.Message.Address,
                Email = context.Message.Email,
            };

            _logger.LogInformation($"Cliente recebido: {customer.Name}");
        }
    }
}