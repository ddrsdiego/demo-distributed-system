using System.Threading.Tasks;

namespace ThinkerThings.Service.CustomerManagement.Domain.AggregateModels.CustomerAggregate
{
    public interface ICustomerRepository
    {
        Task Register(Customer newCustomer);
        Task Update(Customer newCustomer);
        Task<Customer> GetCustomerByEmail(string email);
    }
}