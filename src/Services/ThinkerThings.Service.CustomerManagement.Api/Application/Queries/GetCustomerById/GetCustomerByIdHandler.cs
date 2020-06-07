using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using ThinkerThings.BuildingBlocks.Application;
using ThinkerThings.BuildingBlocks.Cache.Memcached;
using ThinkerThings.Customers.Service.Application.Commands;
using ThinkerThings.Customers.Service.Application.Responses;
using ThinkerThings.Customers.Service.Domain.AggregateModels.CustomerAggregate;

namespace ThinkerThings.Customers.Service.Application.Queries.GetCustomerById
{
    public class GetCustomerByIdHandler : Handler, IRequestHandler<GetCustomerByIdQuery, GetCustomerByIdResponse>
    {
        private readonly ICacheProvider _cacheProvider;
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerByIdHandler(IMediator mediator, ILoggerFactory logger, ICacheProvider cacheProvider, ICustomerRepository customerRepository)
            : base(mediator, logger.CreateLogger<GetCustomerByIdHandler>())
        {
            _cacheProvider = cacheProvider;
            _customerRepository = customerRepository;
        }

        public async Task<GetCustomerByIdResponse> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var response = (GetCustomerByIdResponse)request.Response;

            var custormeResponse = await _cacheProvider.GetValueOrCreate(request.CustomerId,
                                                    async () => await GetCustomerById(request, response));

            if (response.IsFailure)
                return response;

            response.SetPayLoad(custormeResponse);
            return response;
        }

        private async Task<CustomerResponse> GetCustomerById(GetCustomerByIdQuery request, GetCustomerByIdResponse response)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerById(request.CustomerId);
                if (customer is null)
                    response.AddError(Erros.RegisterNewCustomerErrors.CustomerAlreadyRegistered());

                return customer.AdapterEntityToResponse();
            }
            catch (Exception ex)
            {
                response.AddError(Erros.RegisterNewCustomerErrors.CustomerAlreadyRegistered());
                return default(CustomerResponse);
            }
        }
    }
}