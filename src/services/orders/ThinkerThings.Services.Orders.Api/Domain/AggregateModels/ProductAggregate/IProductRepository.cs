using System.Threading.Tasks;

namespace ThinkerThings.Services.Orders.Domain.AggregateModels.ProductAggregate
{
    public interface IProductRepository
    {
        Task<Product> GetProductById(int productId);
    }
}