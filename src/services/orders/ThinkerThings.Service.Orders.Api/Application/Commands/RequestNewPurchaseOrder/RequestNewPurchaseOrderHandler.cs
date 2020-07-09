using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using ThinkerThings.BuildingBlocks.Application;
using ThinkerThings.Orders.Service.Domain.AggregateModels.CustomerAggregate;
using ThinkerThings.Orders.Service.Domain.AggregateModels.OrderAggregate;
using ThinkerThings.Orders.Service.Domain.AggregateModels.ProductAggregate;

namespace ThinkerThings.Orders.Service.Application.Commands.RequestNewPurchaseOrder
{
    public class RequestNewPurchaseOrderHandler : Handler, IRequestHandler<RequestNewPurchaseOrderCommand, RequestNewPurchaseOrderResponse>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;

        public RequestNewPurchaseOrderHandler(IMediator mediator,
                                              ILoggerFactory logger,
                                              IOrderRepository orderRepository,
                                              ICustomerRepository customerRepository,
                                              IProductRepository productRepository)
            : base(mediator, logger.CreateLogger<RequestNewPurchaseOrderHandler>())
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
        }

        public async Task<RequestNewPurchaseOrderResponse> Handle(RequestNewPurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            var response = (RequestNewPurchaseOrderResponse)request.Response;

            var customer = await _customerRepository.GetCustomerById(request.CustomerId);
            if (customer is null)
            {
                return response;
            }

            var product = await _productRepository.GetProductById(0);
            if (product is null)
            {
                return response;
            }

            var newOrder = new Order(customer);
            foreach (var item in request.OrderItems)
            {
                newOrder.AddOrdemItem(item.ProductId, item.Amount);
            }

            await _orderRepository.Register(newOrder);

            return response;
        }
    }
}