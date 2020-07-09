﻿using ThinkerThings.BuildingBlocks.Application;
using ThinkerThings.Customers.Service.Application.Models;

namespace ThinkerThings.Customers.Service.Application.Commands
{
    public class RegisterNewCustomerResponse : Response<CustomerResponse>
    {
        public RegisterNewCustomerResponse(string requestId)
            : base(requestId)
        {
        }
    }
}