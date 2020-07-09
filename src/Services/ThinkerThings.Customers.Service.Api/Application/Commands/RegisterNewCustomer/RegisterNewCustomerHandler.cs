namespace ThinkerThings.Customers.Service.Application.Commands
{
    using MediatR;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using ThinkerThings.BuildingBlocks.Application;
    using ThinkerThings.BuildingBlocks.Cache.Memcached;
    using ThinkerThings.Customers.Service.Application.Models;
    using ThinkerThings.Customers.Service.Domain.AggregateModels.CustomerAggregate;
    using ThinkerThings.Customers.Service.Domain.SeedWorks;
    using ThinkerThings.Customers.Service.Infra.Extensions;

    public class RegisterNewCustomerHandler : CommandHandler, IRequestHandler<RegisterNewCustomerCommand, RegisterNewCustomerResponse>
    {
        private Customer Customer;
        private readonly ICacheRepository _cacheRepository;
        private readonly ICustomerRepository _customerRepository;

        public RegisterNewCustomerHandler(IMediator mediator,
                                          ILoggerFactory logger,
                                          ICustomerRepository customerRepository,
                                          ICacheRepository cacheRepository)
            : base(mediator, logger.CreateLogger<RegisterNewCustomerHandler>())
        {
            _cacheRepository = cacheRepository;
            _customerRepository = customerRepository;
        }

        public async Task<RegisterNewCustomerResponse> Handle(RegisterNewCustomerCommand request, CancellationToken cancellationToken)
        {
            var response = (RegisterNewCustomerResponse)request.Response;

            await GetCustomerByEmail(request, response);
            if (response.IsFailure)
                return response;

            var email = Email.Create(request.Email);
            if (email.IsFailure)
            {
            }

            Customer = new Customer(Guid.NewGuid().ToString("N"), email.Value);

            await _customerRepository.Register(Customer);

            response.SetPayLoad(Customer.AdapterEntityToResponse());

            await _cacheRepository.Set(Customer.CustomerId, response.PayLoad);
            await Mediator.DispatchDomainEvents(Customer);

            return response;
        }

        private async Task GetCustomerByEmail(RegisterNewCustomerCommand request, RegisterNewCustomerResponse response)
        {
            var customer = await _customerRepository.GetCustomerByEmail(request.Email);
            if (customer != null)
            {
                response.AddError(Erros.RegisterNewCustomerErrors
                                            .CustomerAlreadyRegistered()
                                            .AddErroDetail(Erros.RegisterNewCustomerErrors.CustomerAlreadyRegisteredEmail(request.Email)));
                return;
            }
        }
    }

    internal static class CustomerEx
    {
        public static CustomerResponse AdapterEntityToResponse(this Customer customer)
        {
            return new CustomerResponse
            {
                Id = customer.CustomerId,
                Address = customer.Address,
                DateOfBirth = customer.DateOfBirth,
                CreatedAt = customer.CreatedAt,
                Email = customer.Email.Value,
                IsEnable = customer.IsEnable,
                Name = customer.Name,
                UpadatedAt = customer.UpdatedAt,
                Age = customer.Age
            };
        }
    }
}