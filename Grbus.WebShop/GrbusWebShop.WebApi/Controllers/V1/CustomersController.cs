using Grbus.WebShop.Application.Baskets.DTOs;
using Grbus.WebShop.Application.Customers.Commands;
using Grbus.WebShop.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GrbusWebShop.WebApi.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Product.Admin")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(Error))]
        public async Task<IActionResult> Post([FromBody] CreateNewCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }
    }
}
