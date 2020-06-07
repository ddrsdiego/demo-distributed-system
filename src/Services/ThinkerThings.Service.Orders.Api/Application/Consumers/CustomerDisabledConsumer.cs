using MassTransit;
using System.Threading.Tasks;
using ThinkerThings.Service.Messages;

namespace ThinkerThings.Orders.Service.Application.Consumers
{
    public class CustomerDisabledConsumer : IConsumer<CustomerDisabled>
    {
        public async Task Consume(ConsumeContext<CustomerDisabled> context)
        {
            await Task.CompletedTask;
        }
    }
}