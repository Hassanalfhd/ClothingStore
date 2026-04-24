using ClothingStore.Application.Interfaces.Services;
using ClothingStore.Domain.Common;
using MediatR;


namespace ClothingStore.Application.Features.Auth.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<Guid>>
    {

        private readonly IIdentityService _identityService;

        public RegisterCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result<Guid>> Handle(RegisterCommand request, CancellationToken cancellationToken = default)
        {
            return await _identityService.RegisterAsync(request.Email,request.Password,request.FirstName,request.LastName);

        }

    }

}
