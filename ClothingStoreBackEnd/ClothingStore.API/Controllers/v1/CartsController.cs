using ClothingStore.Application.Features.Catalog.Brand.Dtos;
using ClothingStore.Application.Features.Catalog.Cart.Dtos;
using ClothingStore.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/carts")]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }



        [HttpPost]
        public async Task<IActionResult> AddToCart(AddToCartDto dto, CancellationToken cancellationToken)
        {
            var result = await _cartService.AddToCart(dto, cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(result);
        }

    }
}
