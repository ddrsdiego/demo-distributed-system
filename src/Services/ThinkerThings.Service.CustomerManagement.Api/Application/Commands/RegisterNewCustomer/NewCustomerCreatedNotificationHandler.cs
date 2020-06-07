﻿using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using ThinkerThings.BuildingBlocks.Application;
using ThinkerThings.Customers.Service.Domain.AggregateModels.CustomerAggregate;
using ThinkerThings.Service.Messages;

namespace ThinkerThings.Customers.Service.Application.Command
{
    public class NewCustomerCreatedNotificationHandler : Handler, INotificationHandler<NewCustomerCreatedNotification>
    {
        private readonly IBusControl _bus;

        public NewCustomerCreatedNotificationHandler(IMediator mediator, ILoggerFactory logger, IBusControl bus)
            : base(mediator, logger.CreateLogger<NewCustomerCreatedNotificationHandler>())
        {
            _bus = bus;
        }

        public async Task Handle(NewCustomerCreatedNotification notification, CancellationToken cancellationToken)
        {
            await PublishMessage(notification);
        }

        private Task PublishMessage(NewCustomerCreatedNotification notification)
        {
            return _bus.Publish<NewCustomerRegistered>(new
            {
                notification.Customer.Address,
                notification.Customer.Age,
                notification.Customer.CreatedAt,
                notification.Customer.CustomerId,
                notification.Customer.DateOfBirth,
                notification.Customer.Email,
                notification.Customer.IsEnable,
                notification.Customer.Name,
                notification.Customer.UpdatedAt,
            });
        }
    }
}