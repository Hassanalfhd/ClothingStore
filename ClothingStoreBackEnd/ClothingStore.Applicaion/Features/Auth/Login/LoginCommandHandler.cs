using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.Features.Auth.DTOs;
using ClothingStore.Application.Interfaces.Services;
using ClothingStore.Domain.Common;
using MediatR;

namespace ClothingStore.Application.Features.Auth.Login
{
    public class LoginCommandHandler: IRequestHandler<LoginCommand, Result<AuthResponseDto>>
    {

        private readonly IIdentityService _identityService;
        public LoginCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result<AuthResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.LoginAsync(request.Email, request.Password, cancellationToken);
        }
    }
}
