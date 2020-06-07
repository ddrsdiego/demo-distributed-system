using MediatR;
using System;

namespace ThinkerThings.Customers.Service.Domain.AggregateModels.CustomerAggregate
{
    public class CustomerDisabledNotification : INotification
    {
        public CustomerDisabledNotification(string customerId)
        {
            CustomerId = customerId;
        }

        public string CustomerId { get; }
        public DateTime DisabledAt { get; } = DateTime.Now;
    }
}