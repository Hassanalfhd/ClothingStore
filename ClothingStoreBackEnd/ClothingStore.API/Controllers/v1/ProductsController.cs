using ClothingStore.Application.Features.Products.CreateProduct;
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
        public async Task<IActionResult> AddNewProduct(CreateProductCommand createProductCommand)
        {

            var result = await _mediator.Send(createProductCommand);

            if (result.IsFailure) return BadRequest(result);

            return CreatedAtAction("ProductCreated",  new  {result.Value });
        }


    }
}
