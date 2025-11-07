using Grbus.WebShop.Application.Common;
using Grbus.WebShop.Application.Products.Commands;
using Grbus.WebShop.Application.Products.DTOs;
using Grbus.WebShop.Application.Products.Queries;
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
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
           _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<ProductDto>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(Error))]
        public async Task<IActionResult> Get([FromBody] GetAllProductsWithPaginationQuery query)
        {
            var result = await _mediator.Send(query);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(Error))]
        [Authorize]
        [Authorize(Roles = "Product.Admin")]
        public async Task<IActionResult> Post([FromBody] CreateProductCommand command)
        {
            var result = await _mediator.Send(command);

            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }
    }
}
