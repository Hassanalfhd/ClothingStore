using ClothingStore.Application.Features.Products.Commands.CreateProduct;
using ClothingStore.Application.Features.Products.Queries.GetProductById;
using ClothingStore.Application.Features.ProductVariants.Commands.CreateVariant;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.API.Controllers.v1
{

    [ApiController]
    [Route("api/v{version:apiVersion}/Variants")]
    [ApiVersion("1.0")]
    public class ProductVariantsController : ControllerBase
    {


        private readonly IMediator _mediator;

        public ProductVariantsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType( StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddNewProductVariant(
    [FromBody] CreateProductVariantCommand command,
    CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);


            return CreatedAtAction(
                nameof(GetById),
                new { publicId = result.Value },
                result.Value);
        }


        [HttpGet("{publicId:guid}")]
        public async Task<IActionResult> GetById(Guid publicId, CancellationToken cancellationToken)
        {
            return Ok();
        }




    }
}
