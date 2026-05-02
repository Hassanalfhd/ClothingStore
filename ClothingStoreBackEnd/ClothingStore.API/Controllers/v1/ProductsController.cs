using ClothingStore.Application.Features.Products.Commands.CreateProduct;
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
        [ProducesResponseType(StatusCodes.Status400BadRequest )]
        public async Task<IActionResult> AddNewProduct(CreateProductCommand createProductCommand, CancellationToken cancellationToken)
        {

            var result = await _mediator.Send(createProductCommand, cancellationToken);

            if (result.IsFailure) return BadRequest(result);

            return CreatedAtAction(nameof(GetById),  new  {publicId = result.Value }, result.Value);
        }


        [HttpGet("{publicId:guid}")]
        public async Task<IActionResult> GetById(Guid publicId, CancellationToken cancellationToken)
        {

            return Ok();
        }
    }
}
