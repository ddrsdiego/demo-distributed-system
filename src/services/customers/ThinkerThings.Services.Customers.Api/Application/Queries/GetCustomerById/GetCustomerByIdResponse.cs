using ThinkerThings.BuildingBlocks.Application;
using ThinkerThings.Services.Customers.Application.Models;

namespace ThinkerThings.Services.Customers.Application.Queries
{
    public class GetCustomerByIdResponse : Response<CustomerResponse>
    {
        public GetCustomerByIdResponse(string requestId)
            : base(requestId)
        {
        }
    }
}