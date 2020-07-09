using System;
using ThinkerThings.BuildingBlocks.Application;

namespace ThinkerThings.Services.Customers.Application.Commands
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