using ClothingStore.Application.Features.Catalog.Cart.Dtos;
using ClothingStore.Application.Interfaces.Services;
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

        [HttpDelete]
        public async Task<IActionResult> RemoveFromCart(
            ManageCartItemQuantityDto dto,
            CancellationToken cancellationToken)
        {
            var result = await _cartService.RemoveFromCart(dto, cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return NoContent();
        
        }


        [HttpGet()]
        public async Task<IActionResult> GetCart(
    Guid UserId,
    CancellationToken cancellationToken)
        {
            var result = await _cartService.GetCartAsync(UserId, cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(result);
        }


        [HttpDelete("Clear-Cart")]
        public async Task<IActionResult> ClearCart(
    Guid UserId,
    CancellationToken cancellationToken)
        {
            var result = await _cartService.ClearCart(UserId, cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpPost("increase-quantity")]
        public async Task<IActionResult> IncreaseQuantity(
    [FromBody] ManageCartItemQuantityDto dto,
    CancellationToken cancellationToken)
        {
            var result = await _cartService.IncreaseQuantity(dto, cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(result);
        }

        [HttpPost("decrease-quantity")]
        public async Task<IActionResult> DecreaseQuantity(
        [FromBody] ManageCartItemQuantityDto dto,
        CancellationToken cancellationToken)
        {
            var result = await _cartService.DecreaseQuantity(dto, cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(result);
        }
    }
}
