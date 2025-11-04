using Grbus.WebShop.Application.Baskets.Commands;
using Grbus.WebShop.Application.Baskets.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Get([FromQuery] string id)
        {
            var query = new GetBasketByIdQuery { CustomerEmail = id };
            var result = await _mediator.Send(query);
            if(result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        [HttpPatch("additems")]
        public async Task<IActionResult> AddItemsToBasket([FromBody] AddItemsToBasketCommand command)
        {
            var result = await _mediator.Send(command);
            if(result.IsSuccess)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        [HttpPatch("changequanity")]
        public async Task<IActionResult> ChangeQuantity([FromBody] ChangeBasketItemQuantityCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        [HttpPatch("removeitem")]
        public async Task<IActionResult> RemoveItem([FromBody] RemoveItemFromBasketCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Error);
            }
        }
    }
}
