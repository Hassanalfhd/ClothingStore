
using FluentValidation;

namespace ClothingStore.Application.Features.Auth.Register;
public  class RegisterValidator: AbstractValidator<RegisterCommand>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();

        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);

        RuleFor(x=>x.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x=>x.LastName).NotEmpty().MaximumLength(50);
    }
}

        

