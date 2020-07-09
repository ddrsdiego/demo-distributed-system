using System.Threading.Tasks;

namespace ThinkerThings.Orders.Service.Domain.AggregateModels.OrderAggregate
{
    public interface IOrderRepository
    {
        Task Register(Order order);
    }
}