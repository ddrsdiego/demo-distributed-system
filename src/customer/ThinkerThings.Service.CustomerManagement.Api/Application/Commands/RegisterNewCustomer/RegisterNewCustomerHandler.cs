using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using ThinkerThings.BuildingBlocks.Application;
using ThinkerThings.BuildingBlocks.Cache.Memcached;
using ThinkerThings.Service.CustomerManagement.Domain.AggregateModels.CustomerAggregate;

namespace ThinkerThings.Service.CustomerManagement.Application.Commands.RegisterNewCustomer
{
    public class RegisterNewCustomerHandler : CommandHandler, IRequestHandler<RegisterNewCustomerCommand, RegisterNewCustomerResponse>
    {
        private readonly ICacheRepository _cacheRepository;
        private readonly ICustomerRepository _customerRepository;

        public RegisterNewCustomerHandler(IMediator mediator, ILoggerFactory logger,
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

            var newCustomer = request.AdpaterCommandToEntity();

            await _customerRepository.Register(request.AdpaterCommandToEntity());
            response.SetPayLoad(newCustomer.Create());

            await _cacheRepository.Set<RegisterNewCustomerResponse>(newCustomer.CustomerId, response);

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

    public static class CustomerEx
    {
        public static CustomerResponse Create(this Customer customer)
        {
            return new CustomerResponse
            {
                Id = customer.CustomerId,
                Address = customer.Address,
                BirthDate = customer.BirthDate,
                CreatedAt = customer.CreatedAt,
                Email = customer.Email,
                IsEnable = customer.IsEnable,
                Name = customer.Name,
                UpadatedAt = customer.UpadatedAt,
            };
        }
    }
}