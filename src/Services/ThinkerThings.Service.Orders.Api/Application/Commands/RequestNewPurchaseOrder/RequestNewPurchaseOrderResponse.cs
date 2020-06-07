using ThinkerThings.BuildingBlocks.Application;

namespace ThinkerThings.Orders.Service.Application.Commands.RequestNewPurchaseOrder
{
    public class RequestNewPurchaseOrderResponse : Response
    {
        public RequestNewPurchaseOrderResponse(string requestId)
            : base(requestId)
        {
        }
    }
}