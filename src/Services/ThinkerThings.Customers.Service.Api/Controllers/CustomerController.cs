using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ThinkerThings.Customers.Service.Application.Commands;
using ThinkerThings.Customers.Service.Application.Queries.GetCustomerById;

namespace ThinkerThings.Customers.Service.Api.Controllers
{
    [ApiController]
    [Route("customers")]
    public class CustomerController : Controller
    {
        private readonly IMediator _mediator;

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
        public async Task<IActionResult> GetCustomerById([FromQuery] string id)
        {
            var response = await _mediator.Send(new GetCustomerByIdQuery(id));
            if (response.IsFailure)
                return BadRequest(response.ErrorResponse);

            return Created("", response.PayLoad);
        }
    }
}