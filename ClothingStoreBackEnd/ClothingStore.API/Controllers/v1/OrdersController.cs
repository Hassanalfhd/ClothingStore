using ClothingStore.Application.Features.Orders.Commands.PlaceOrder;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout(
            [FromBody] PlaceOrderCommand request,
            CancellationToken ct)
        {
            var command = new PlaceOrderCommand(
                request.UserId,
                request.RecipientName,
                request.PhoneNumber,
                request.City,
                request.AddressLine
            );


            var result = await _mediator.Send(command, ct);

            if (result.IsFailure)
                return Problem(
                 detail: result.Error,
                 statusCode: StatusCodes.Status400BadRequest);

            return Ok(new
            {
                OrderPublicId = result.Value

            });
        }

    }
    
 }

