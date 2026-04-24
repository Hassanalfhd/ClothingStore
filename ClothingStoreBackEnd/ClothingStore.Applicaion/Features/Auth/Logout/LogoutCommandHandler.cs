using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.Interfaces.Services;
using ClothingStore.Domain.Common;
using MediatR;

namespace ClothingStore.Application.Features.Auth.Logout
{
    public class LogoutCommandHandler: IRequestHandler<LogoutCommand, Result>
    {

        private readonly IIdentityService _identityService;

        public LogoutCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.LogoutAsync(request.refreshToken, cancellationToken);
        }
    }
}
