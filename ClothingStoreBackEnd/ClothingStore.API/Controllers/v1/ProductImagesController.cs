using ClothingStore.Application.Features.ProductImages.Commands.CreateImage;
using ClothingStore.Application.Features.ProductImages.Commands.DeleteImage;
using ClothingStore.Application.Features.ProductImages.Commands.ReorderImages;
using ClothingStore.Application.Features.ProductImages.Commands.SetPrimaryImage;
using ClothingStore.Application.Features.Products.Commands.CreateProduct;
using ClothingStore.Application.Features.Products.Commands.UpdateProduct;
using ClothingStore.Application.Features.Products.Queries.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/ProductsImages")]
    [ApiVersion("1.0")]
    public class ProductImagesController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ProductImagesController(IMediator mediator)
        {
            _mediator = mediator;
        }




        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddNewProductImage(CreateProductImageCommand command, CancellationToken cancellationToken)
        {

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsFailure) return BadRequest(result);

            return CreatedAtAction(nameof(GetById), new { publicId = result.Value }, result);
        }


        [HttpPatch("{id:guid}/set-primary")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SetPrimaryImage(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new SetPrimaryProductImageCommand
            {
                ProductImageId = id
            }, cancellationToken);

            if (result.IsFailure) return BadRequest(result);

            return Ok(result);
        }


        [HttpGet("{publicId:guid}")]
        public async Task<IActionResult> GetById(Guid publicId, CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProductImage(
    Guid id,
    CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new DeleteProductImageCommand
                {
                    ProductImageId = id
                },
                cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("reorder")]
        public async Task<IActionResult> ReorderImages(
    [FromBody] ReorderProductImagesCommand command,
    CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

    }
}
