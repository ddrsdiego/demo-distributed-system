using System;
using System.Collections.Generic;
using System.Linq;
using ThinkerThings.Orders.Service.Domain.AggregateModels.CustomerAggregate;

namespace ThinkerThings.Orders.Service.Domain.AggregateModels.OrderAggregate
{
    public class Order
    {
        private readonly List<OrderItem> _orderItems;

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
            {
                return;
            }

            _orderItems.Add(orderItem);
        }
    }
}