namespace ThinkerThings.Services.Customers.Application.Commands
{
    using MediatR;
    using Microsoft.Extensions.Logging;
    using System.Threading;
    using System.Threading.Tasks;
    using ThinkerThings.BuildingBlocks.Application;
    using ThinkerThings.BuildingBlocks.Cache.Memcached;
    using ThinkerThings.Services.Customers.Domain.AggregateModels.CustomerAggregate;
    using ThinkerThings.Services.Customers.Infra.Extensions;

    public class DisableCustomerHandler : Handler, IRequestHandler<DisableCustomerCommand, DisableCustomerResponse>
    {
        private Customer Customer;
        private readonly ICacheRepository _cacheRepository;
        private readonly ICustomerRepository _customerRepository;

        public DisableCustomerHandler(IMediator mediator,
                                      ILoggerFactory logger,
                                      ICustomerRepository customerRepository,
                                      ICacheRepository cacheRepository)
            : base(mediator, logger.CreateLogger<DisableCustomerHandler>())
        {
            _cacheRepository = cacheRepository;
            _customerRepository = customerRepository;
        }

        public async Task<DisableCustomerResponse> Handle(DisableCustomerCommand request, CancellationToken cancellationToken)
        {
            var response = (DisableCustomerResponse)request.Response;

            await GetCustomerById(request, response);
            if (response.IsFailure)
                return response;

            ExecuteDisableCustomer(request, response);
            if (response.IsFailure)
                return response;

            await _customerRepository.Disable(Customer);

            var customerResponse = Customer.AdapterEntityToResponse();

            await _cacheRepository.Set(Customer.CustomerId, customerResponse);
            await Mediator.DispatchDomainEvents(Customer);

            return response;
        }

        private void ExecuteDisableCustomer(DisableCustomerCommand request, DisableCustomerResponse response)
        {
            var updateStatus = Customer.Disable();
            if (updateStatus.IsFailure)
            {
                response.AddError(Errors.General.InvalidCommandArguments()
                                                 .AddErroDetail(Errors.General.InvalidArgument("CustomerIsAlreadyDisabled", string.Join('|', updateStatus.Messages))));
            }
        }

        private async Task GetCustomerById(DisableCustomerCommand request, DisableCustomerResponse response)
        {
            Customer = await _customerRepository.GetCustomerById(request.CustomerId);
            if (Customer is null)
            {
                response.AddError(Errors.General.NotFound(nameof(Customer), request.CustomerId));
                return;
            }
        }
    }
}