using ClothingStore.Application.Features.Auth;
using ClothingStore.Application.Features.Auth.DTOs;
using ClothingStore.Domain.Common;
using MediatR;

namespace ClothingStore.Application.Interfaces.Services
{
    public interface IIdentityService
    {
        Task<Result<Guid>> RegisterAsync(string Email,string  Password,string FirstName,string LastName, CancellationToken cancellationToken = default);


        Task<Result<AuthResponseDto>> LoginAsync(string Email,string Password, CancellationToken cancellationToken =default);

        Task<Result<AuthResponseDto>> RefreshTokenAsync(string refreshToken, string AccessToken, CancellationToken cancellationToken = default);

        Task<Result> LogoutAsync(string refreshToken, CancellationToken cancellationToken = default);


    }
}
