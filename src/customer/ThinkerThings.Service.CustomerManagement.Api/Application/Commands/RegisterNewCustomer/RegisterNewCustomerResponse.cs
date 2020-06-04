using System;
using ThinkerThings.BuildingBlocks.Application;

namespace ThinkerThings.Service.CustomerManagement.Application.Commands.RegisterNewCustomer
{
    public class RegisterNewCustomerResponse : Response
    {
        public RegisterNewCustomerResponse(string requestId)
            : base(requestId)
        {
        }

        public CustomerResponse PayLoad { get; private set; }

        public void SetPayLoad(CustomerResponse customerResponse) => PayLoad = customerResponse;
    }

    public class CustomerResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpadatedAt { get; set; }
        public bool IsEnable { get; set; }
    }
}