using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.Features.Auth.DTOs;
using ClothingStore.Application.Interfaces.Services;
using ClothingStore.Domain.Common;
using MediatR;

namespace ClothingStore.Application.Features.Auth.RefreshToken
{
    public class RefreshTokenCommandHandler: IRequestHandler<RefreshTokenCommand, Result<AuthResponseDto>>
    {
        private readonly IIdentityService _identityService;

        public RefreshTokenCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }


        public async Task<Result<AuthResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.RefreshTokenAsync(request.RefreshToken, request.AccessToken, cancellationToken);
        }
    }
}
