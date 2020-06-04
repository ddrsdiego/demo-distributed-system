using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ThinkerThings.Service.CustomerManagement.Application.Commands.RegisterNewCustomer;

namespace ThinkerThings.Service.CustomerManagement.Api.Controllers
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
    }
}