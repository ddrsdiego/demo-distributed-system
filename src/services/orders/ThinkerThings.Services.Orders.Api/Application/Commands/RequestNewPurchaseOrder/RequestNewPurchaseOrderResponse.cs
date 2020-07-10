using ThinkerThings.BuildingBlocks.Application;

namespace ThinkerThings.Services.Orders.Application.Commands
{
    public class RequestNewPurchaseOrderResponse : Response
    {
        public RequestNewPurchaseOrderResponse(string requestId)
            : base(requestId)
        {
        }
    }
}