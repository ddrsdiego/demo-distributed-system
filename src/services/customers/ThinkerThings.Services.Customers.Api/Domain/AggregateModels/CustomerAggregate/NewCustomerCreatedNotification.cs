using MediatR;

namespace ThinkerThings.Services.Customers.Domain.AggregateModels.CustomerAggregate
{
    public class NewCustomerCreatedNotification : INotification
    {
        public NewCustomerCreatedNotification(Customer customer)
        {
            Customer = customer;
        }

        public Customer Customer { get; }
    }
}