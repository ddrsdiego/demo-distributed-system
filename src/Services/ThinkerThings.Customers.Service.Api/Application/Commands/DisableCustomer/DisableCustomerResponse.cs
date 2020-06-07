using System;
using ThinkerThings.BuildingBlocks.Application;

namespace ThinkerThings.Customers.Service.Application.Commands
{
    public class DisableCustomerResponse : Response
    {
        public DisableCustomerResponse(string requestId)
            : base(requestId)
        {
        }

        public DateTime DisabledAt { get; }
    }
}