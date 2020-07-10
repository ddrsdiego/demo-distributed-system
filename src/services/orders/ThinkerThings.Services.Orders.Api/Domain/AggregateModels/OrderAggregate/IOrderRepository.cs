using System.Threading.Tasks;

namespace ThinkerThings.Services.Orders.Domain.AggregateModels.OrderAggregate
{
    public interface IOrderRepository
    {
        Task Register(Order order);
    }
}