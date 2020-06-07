using MediatR;

namespace ThinkerThings.Customers.Service.Domain.AggregateModels.CustomerAggregate
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