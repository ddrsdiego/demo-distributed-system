namespace ThinkerThings.Services.Customers.Application.Commands
{
    using MediatR;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using ThinkerThings.BuildingBlocks.Application;
    using ThinkerThings.BuildingBlocks.Cache.Memcached;
    using ThinkerThings.Services.Customers.Application.Models;
    using ThinkerThings.Services.Customers.Domain.AggregateModels.CustomerAggregate;
    using ThinkerThings.Services.Customers.Domain.SeedWorks;
    using ThinkerThings.Services.Customers.Infra.Extensions;

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

            Customer = new Customer(Guid.NewGuid().ToString("N"), email.Value)
            {
                Address = request.Address,
                BirthDate = request.BirthDate,
                Name = request.Name,
            };

            await _customerRepository.Register(Customer);

            response.SetPayLoad(Customer.AdapterEntityToResponse());

            await _cacheRepository.Set(Customer.CustomerId, response.PayLoad);
            await Mediator.DispatchDomainEvents(Customer);

            return response;
        }

        private async Task GetCustomerByEmail(RegisterNewCustomerCommand request, RegisterNewCustomerResponse response)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerByEmail(request.Email);
                if (!string.IsNullOrEmpty(customer.CustomerId))
                {
                    response.AddError(Errors.RegisterNewCustomerErrors
                                                .CustomerAlreadyRegistered()
                                                .AddErroDetail(Errors.RegisterNewCustomerErrors.CustomerAlreadyRegisteredEmail(request.Email)));
                    return;
                }
            }
            catch (Exception ex)
            {
                response.AddError(Errors.General.InternalProcessError("GetCustomerByEmail", ex.Message));
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
                DateOfBirth = customer.BirthDate,
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