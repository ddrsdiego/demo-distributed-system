using System.Threading.Tasks;

namespace ThinkerThings.Services.Orders.Domain.AggregateModels.CustomerAggregate
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerById(string customerId);
    }
}