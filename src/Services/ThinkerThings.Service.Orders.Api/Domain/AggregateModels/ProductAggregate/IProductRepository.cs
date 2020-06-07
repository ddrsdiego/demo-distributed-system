using System.Threading.Tasks;

namespace ThinkerThings.Orders.Service.Domain.AggregateModels.ProductAggregate
{
    public interface IProductRepository
    {
        Task<Product> GetProductById(int productId);
    }
}