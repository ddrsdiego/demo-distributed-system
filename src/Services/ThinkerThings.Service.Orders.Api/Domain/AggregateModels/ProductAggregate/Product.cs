namespace ThinkerThings.Orders.Service.Domain.AggregateModels.ProductAggregate
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string UnitPrice { get; set; }
        public bool IsEnable { get; set; }
    }
}