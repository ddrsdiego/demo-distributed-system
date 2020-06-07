using MediatR;
using System.Linq;
using System.Threading.Tasks;
using ThinkerThings.Customers.Service.Domain.SeedWorks;

namespace ThinkerThings.Customers.Service.Infra.Extensions
{
    public static class MediatorExtension
    {
        public static async Task DispatchDomainEvents(this IMediator mediator, Entity entity)
        {
            if (entity.DomainEvents.Count == 0)
                return;

            var tasks = entity.DomainEvents.Select(async domainEvent => await mediator.Publish(domainEvent));

            await Task.WhenAll(tasks);

            entity.ClearDomainEvents();
        }
    }
}