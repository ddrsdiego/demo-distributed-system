using ThinkerThings.BuildingBlocks.Application;
using ThinkerThings.Customers.Service.Application.Models;

namespace ThinkerThings.Customers.Service.Application.Queries.GetCustomerById
{
    public class GetCustomerByIdResponse : Response<CustomerResponse>
    {
        public GetCustomerByIdResponse(string requestId)
            : base(requestId)
        {
        }
    }
}