using System.Threading.Tasks;

namespace ThinkerThings.Orders.Service.Domain.AggregateModels.CustomerAggregate
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerById(string customerId);
    }
}