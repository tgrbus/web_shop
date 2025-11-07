using Grbus.WebShop.Application.Baskets.Commands;
using Grbus.WebShop.Application.Baskets.DTOs;
using Grbus.WebShop.Application.Baskets.Queries;
using Grbus.WebShop.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GrbusWebShop.WebApi.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BasketsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(BasketDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(Error))]
        public async Task<IActionResult> Get([FromQuery] string id)
        {
            var query = new GetBasketByIdQuery { CustomerEmail = id };
            var result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPatch("additems")]

        public async Task<IActionResult> AddItemsToBasket([FromBody] AddItemsToBasketCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }

        [HttpPatch("changequantity")]
        public async Task<IActionResult> ChangeQuantity([FromBody] ChangeBasketItemQuantityCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }

        [HttpPatch("removeitem")]
        public async Task<IActionResult> RemoveItem([FromBody] RemoveItemFromBasketCommand command)
        {
            var result = await _mediator.Send(command);
            
            return result.IsSuccess ? Ok() : BadRequest(result.Error);  
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory([FromBody] GetBasketHistoryWithPaginationQuery query)
        {
            var result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
    }
}
