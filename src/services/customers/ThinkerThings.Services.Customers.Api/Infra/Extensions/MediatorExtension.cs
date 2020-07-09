namespace ThinkerThings.Services.Customers.Infra.Extensions
{
    using MediatR;
    using System.Linq;
    using System.Threading.Tasks;
    using ThinkerThings.Services.Customers.Domain.SeedWorks;

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