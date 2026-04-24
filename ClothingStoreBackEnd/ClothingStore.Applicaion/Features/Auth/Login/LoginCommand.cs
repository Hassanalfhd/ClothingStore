using ClothingStore.Application.Features.Auth.DTOs;
using ClothingStore.Domain.Common;
using MediatR;


namespace ClothingStore.Application.Features.Auth.Login
{
    public class LoginCommand : IRequest<Result<AuthResponseDto>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public LoginCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
