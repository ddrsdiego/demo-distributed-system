namespace ThinkerThings.Services.Orders.Domain.AggregateModels.CustomerAggregate
{
    public class Customer
    {
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}