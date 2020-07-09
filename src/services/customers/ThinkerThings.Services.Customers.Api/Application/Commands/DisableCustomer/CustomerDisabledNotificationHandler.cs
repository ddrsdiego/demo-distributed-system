﻿namespace ThinkerThings.Services.Customers.Application.Commands
{
    using MassTransit;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using System.Threading;
    using System.Threading.Tasks;
    using ThinkerThings.BuildingBlocks.Application;
    using ThinkerThings.Services.Customers.Domain.AggregateModels.CustomerAggregate;
    using ThinkerThings.Services.Messages;

    public class CustomerDisabledNotificationHandler : Handler, INotificationHandler<CustomerDisabledNotification>
    {
        private readonly IBusControl _busControl;

        public CustomerDisabledNotificationHandler(IMediator mediator, ILoggerFactory logger, IBusControl busControl)
            : base(mediator, logger.CreateLogger<CustomerDisabledNotificationHandler>())
        {
            _busControl = busControl;
        }

        public async Task Handle(CustomerDisabledNotification notification, CancellationToken cancellationToken)
        {
            await _busControl.Publish<CustomerDisabled>(new
            {
                Id = notification.CustomerId,
                notification.DisabledAt
            });
        }
    }
}