using MediatR;
using Microsoft.AspNetCore.Mvc;
using ClothingStore.Application.Features.Auth.Register;
using ClothingStore.Application.Features.Auth.Logout;
using ClothingStore.Application.Features.Auth.Login;
using ClothingStore.Application.Features.Auth.RefreshToken;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }



    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result.Error);


        return Ok(result);
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(
      LoginCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(
        RefreshTokenCommand command)
    {
        var result = await _mediator.Send(command);
        
        return Ok(result);
    }



    [HttpPost("logout")]
    public async Task<IActionResult> Logout(
    LogoutCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}