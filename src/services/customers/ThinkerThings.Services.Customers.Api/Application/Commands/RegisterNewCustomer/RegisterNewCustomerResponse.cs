using ThinkerThings.BuildingBlocks.Application;
using ThinkerThings.Services.Customers.Application.Models;

namespace ThinkerThings.Services.Customers.Application.Commands
{
    public class RegisterNewCustomerResponse : Response<CustomerResponse>
    {
        public RegisterNewCustomerResponse(string requestId)
            : base(requestId)
        {
        }
    }
}