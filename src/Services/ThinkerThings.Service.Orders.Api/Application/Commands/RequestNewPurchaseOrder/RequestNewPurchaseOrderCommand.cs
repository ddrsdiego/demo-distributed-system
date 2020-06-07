using MediatR;
using System.Collections.Generic;
using ThinkerThings.BuildingBlocks.Application;

namespace ThinkerThings.Orders.Service.Application.Commands.RequestNewPurchaseOrder
{
    public class RequestNewPurchaseOrderCommand : Request, IRequest<RequestNewPurchaseOrderResponse>
    {
        public RequestNewPurchaseOrderCommand(string customerId, List<OrderItemRequest> orderItems)
        {
            CustomerId = customerId;
            OrderItems = orderItems;
        }

        public string CustomerId { get; }
        public List<OrderItemRequest> OrderItems { get; }
        public override Response Response => new RequestNewPurchaseOrderResponse(RequestId);
    }

    public class OrderItemRequest
    {
        public OrderItemRequest(int productId, decimal amount)
        {
            Amount = amount;
            ProductId = productId;
        }

        public int ProductId { get; }
        public decimal Amount { get; }
    }
}