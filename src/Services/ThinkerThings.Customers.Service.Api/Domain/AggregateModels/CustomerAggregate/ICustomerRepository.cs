using System.Threading.Tasks;

namespace ThinkerThings.Customers.Service.Domain.AggregateModels.CustomerAggregate
{
    public interface ICustomerRepository
    {
        Task Disable(Customer newCustomer);

        Task Register(Customer newCustomer);

        Task<Customer> GetCustomerByEmail(string email);

        Task<Customer> GetCustomerById(string customerId);
    }
}