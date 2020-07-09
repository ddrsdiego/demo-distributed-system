using MediatR;
using ThinkerThings.BuildingBlocks.Application;

namespace ThinkerThings.Services.Customers.Application.Commands
{
    public class DisableCustomerCommand : Request, IRequest<DisableCustomerResponse>
    {
        public DisableCustomerCommand(string customerId)
        {
            CustomerId = customerId;
        }

        public string CustomerId { get; }

        public override Response Response => new DisableCustomerResponse(RequestId);
    }
}