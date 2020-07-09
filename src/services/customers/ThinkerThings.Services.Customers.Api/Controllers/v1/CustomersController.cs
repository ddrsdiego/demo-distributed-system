namespace ThinkerThings.Services.Customers.Api.Controllers.v1
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;
    using System.Threading.Tasks;
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
        public async Task<IActionResult> RegisterNewCustomer(RegisterNewCustomerCommand command)
        {
            var response = await _mediator.Send(command);
            if (response.IsFailure)
                return BadRequest(response.ErrorResponse);

            return Created("", new { response.PayLoad.Id, response.PayLoad.Name, response.PayLoad.CreatedAt });
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
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(CustomerResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCustomerById([FromQuery] string id)
        {
            var response = await _mediator.Send(new GetCustomerByIdQuery(id));
            if (response.IsFailure)
                return BadRequest(response.ErrorResponse);

            return Created("", response.PayLoad);
        }
    }
}