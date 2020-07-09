namespace ThinkerThings.Orders.Service.Domain.AggregateModels.OrderAggregate
{
    public class OrderItem
    {
        public OrderItem(int productId, decimal amount)
        {
            Amount = amount;
            ProductId = productId;
        }

        public int ProductId { get; }
        public decimal Amount { get; }
    }
}