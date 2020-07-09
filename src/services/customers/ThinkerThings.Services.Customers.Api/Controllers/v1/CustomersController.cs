namespace ThinkerThings.Services.Customers.Api.Controllers.v1
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;
    using System.Threading.Tasks;
    using ThinkerThings.BuildingBlocks.Cache.Memcached;
    using ThinkerThings.Services.Customers.Application.Commands;
    using ThinkerThings.Services.Customers.Application.Models;
    using ThinkerThings.Services.Customers.Application.Queries;

    [ApiController]
    [ApiVersion(API_VERSION)]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/customers")]
    public class CustomerController : Controller
    {
        private readonly IMediator _mediator;
        private const string API_VERSION = "1";

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(CustomerResponse), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> RegisterNewCustomer(RegisterNewCustomerCommand command)
        {
            var response = await _mediator.Send(command);
            if (response.IsFailure)
                return BadRequest(response.ErrorResponse);

            return Created($"{Request.Scheme}://{Request.Host}/api/v{API_VERSION}/customers/{response.PayLoad.Id}", response.PayLoad);
        }

        [HttpPut]
        [Route("{customerId}")]
        public async Task<IActionResult> DisableCustomer(string customerId)
        {
            var response = await _mediator.Send(new DisableCustomerCommand(customerId));
            if (response.IsFailure)
                return BadRequest(response.ErrorResponse);

            return Ok();
        }

        [HttpGet]
        [Route("{customerId}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(CustomerResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCustomerById(string customerId)
        {
            var response = await _mediator.Send(new GetCustomerByIdQuery(customerId));
            if (response.IsFailure)
                return BadRequest(response.ErrorResponse);

            return Ok(response.PayLoad);
        }

        [HttpGet]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(CustomerResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCustomerByEmail([FromServices] ICacheProvider cacheProvider, [FromQuery] string email)
        {
            var response = await cacheProvider.Get<CustomerResponse>(email);
            if (response.Equals(default))
                return NotFound();

            return Ok(response);
        }
    }
}