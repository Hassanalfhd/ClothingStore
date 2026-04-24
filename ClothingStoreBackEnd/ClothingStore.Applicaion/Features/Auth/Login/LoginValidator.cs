using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ClothingStore.Application.Features.Auth.Login
{
    public class LoginValidator: AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            RuleFor(x=>x.Email).NotEmpty().EmailAddress();
            RuleFor(x=>x.Password).NotEmpty().MinimumLength(6);
        }
    }
}
