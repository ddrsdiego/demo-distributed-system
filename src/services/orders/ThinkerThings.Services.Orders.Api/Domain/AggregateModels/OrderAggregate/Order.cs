namespace ThinkerThings.Services.Orders.Domain.AggregateModels.OrderAggregate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ThinkerThings.Services.Orders.Domain.AggregateModels.CustomerAggregate;

    public class Order
    {
        private readonly List<OrderItem> _orderItems = new List<OrderItem>();

        public Order(Customer customer)
        {
            Customer = customer;
        }

        public string OrderId { get; } = Guid.NewGuid().ToString("N");
        public Customer Customer { get; }
        public DateTime CreatedAt { get; } = DateTime.Now;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public void AddOrdemItem(int productId, decimal amount)
        {
            var orderItem = new OrderItem(productId, amount);
            var productAlreadyRegistered = _orderItems.SingleOrDefault(x => x.ProductId.Equals(orderItem.ProductId));

            if (productAlreadyRegistered != null)
                return;

            _orderItems.Add(orderItem);
        }
    }
}