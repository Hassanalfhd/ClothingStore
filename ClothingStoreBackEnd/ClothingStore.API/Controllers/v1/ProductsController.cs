using System.Formats.Asn1;
using ClothingStore.Application.Features.Products.Commands.CreateProduct;
using ClothingStore.Application.Features.Products.Commands.ToggleActivateProduct;
using ClothingStore.Application.Features.Products.Commands.UpdateProduct;
using ClothingStore.Application.Features.Products.Queries.GetProductById;
using ClothingStore.Application.Features.Products.Queries.GetProducts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/Products")]
    [ApiVersion("1.0")]
    public class ProductsController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }



        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddNewProduct(CreateProductCommand command, CancellationToken cancellationToken)
        {

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsFailure) return BadRequest(result);

            return CreatedAtAction(nameof(GetById), new { publicId = result.Value }, result.Value);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsFailure) return BadRequest(result);

            return Ok(result);

        }
        [HttpGet("{publicId:guid}")]
        public async Task<IActionResult> GetById(Guid publicId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(publicId), cancellationToken);

            if (result.IsFailure) return NotFound(result);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(
    [FromQuery] GetProductsQuery query,
    CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }


        [HttpPatch]
        public async Task<IActionResult> ToggleActivate(Guid publicId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new ToggleActivateCommand (publicId), cancellationToken);

            if (result.IsFailure) return BadRequest(result);

            return Ok(result);
        }


    }
    
}
