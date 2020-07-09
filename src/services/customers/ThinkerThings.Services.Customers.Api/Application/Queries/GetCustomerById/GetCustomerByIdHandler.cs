namespace ThinkerThings.Services.Customers.Application.Queries
{
    using MediatR;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using ThinkerThings.BuildingBlocks.Application;
    using ThinkerThings.BuildingBlocks.Cache.Memcached;
    using ThinkerThings.Services.Customers.Application.Commands;
    using ThinkerThings.Services.Customers.Application.Models;
    using ThinkerThings.Services.Customers.Domain.AggregateModels.CustomerAggregate;

    public class GetCustomerByIdHandler : Handler, IRequestHandler<GetCustomerByIdQuery, GetCustomerByIdResponse>
    {
        private CustomerResponse CustomerResponse;
        private readonly ICacheProvider _cacheProvider;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICacheRepository _cacheRepository;

        public GetCustomerByIdHandler(IMediator mediator, ILoggerFactory logger, ICacheProvider cacheProvider, ICacheRepository cacheRepository, ICustomerRepository customerRepository)
            : base(mediator, logger.CreateLogger<GetCustomerByIdHandler>())
        {
            _cacheProvider = cacheProvider;
            _customerRepository = customerRepository;
            _cacheRepository = cacheRepository;
        }

        public async Task<GetCustomerByIdResponse> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var response = (GetCustomerByIdResponse)request.Response;

            await GetCustomerById(request, response);
            if (response.IsFailure)
                return response;

            if (response.IsFailure)
                return response;

            response.SetPayLoad(CustomerResponse);
            return response;
        }

        private async Task GetCustomerById(GetCustomerByIdQuery request, GetCustomerByIdResponse response)
        {
            try
            {
                CustomerResponse = await GetCustomerByIdFromCacheStrategy(async () =>
                {
                    var customer = await _customerRepository.GetCustomerById(request.CustomerId);
                    if (customer is null)
                        return default;

                    return customer.AdapterEntityToResponse();
                }, async () => await _cacheProvider.Get<CustomerResponse>(request.CustomerId));

                if (string.IsNullOrEmpty(CustomerResponse.Id))
                {
                    response.AddError(Errors.General.NotFound(nameof(Customer), request.CustomerId));
                }
            }
            catch (Exception ex)
            {
                response.AddError(Errors.General.InternalProcessError("GetCustomerById", ex.Message));
            }
        }

        private async Task<CustomerResponse> GetCustomerByIdFromCacheStrategy(Func<Task<CustomerResponse>> fromRepo, Func<Task<CustomerResponse>> fromCache)
        {
            CustomerResponse customerResponse = await fromCache();
            if (!string.IsNullOrEmpty(customerResponse.Id))
                return customerResponse;

            customerResponse = await fromRepo();
            if (!string.IsNullOrEmpty(customerResponse.Id))
            {
                await _cacheRepository.Set(customerResponse.Id, customerResponse);
                await _cacheRepository.Set(customerResponse.Email, customerResponse);
            }

            return customerResponse;
        }
    }
}