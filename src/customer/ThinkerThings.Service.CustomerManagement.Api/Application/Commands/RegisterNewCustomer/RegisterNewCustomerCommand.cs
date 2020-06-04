using MediatR;
using System;
using ThinkerThings.BuildingBlocks.Application;
using ThinkerThings.Service.CustomerManagement.Domain.AggregateModels.CustomerAggregate;

namespace ThinkerThings.Service.CustomerManagement.Application.Commands.RegisterNewCustomer
{
    public class RegisterNewCustomerCommand : Request, IRequest<RegisterNewCustomerResponse>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public override Response Response => new RegisterNewCustomerResponse(RequestId);
    }

    public static class RegisterNewCustomerCommandEx
    {
        public static Customer AdpaterCommandToEntity(this RegisterNewCustomerCommand command)
        {
            return new Customer
            {
                Name = command.Name,
                Email = command.Email,
                Address = command.Address,
                BirthDate = command.BirthDate,
            };
        }
    }
}