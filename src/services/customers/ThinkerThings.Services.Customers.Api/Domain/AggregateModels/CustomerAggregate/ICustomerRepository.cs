namespace ThinkerThings.Services.Customers.Domain.AggregateModels.CustomerAggregate
{
    using System.Threading.Tasks;

    public interface ICustomerRepository
    {
        Task Disable(Customer customer);

        Task Register(Customer newCustomer);

        Task<Customer> GetCustomerByEmail(string email);

        Task<Customer> GetCustomerById(string customerId);
    }
}