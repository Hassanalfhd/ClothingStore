using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ClothingStore.Application.Features.Orders.Commands.PlaceOrder
{
    public class PlaceOrderCommandValidator: AbstractValidator<PlaceOrderCommand>
    {
        public PlaceOrderCommandValidator() 
        { 
            RuleFor(x=>x.UserId)
                .NotEmpty()
                .WithMessage("User Id Is required.");

            RuleFor(x=>x.RecipientName)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("RecipientName Is required.");

            RuleFor(x=>x.AddressLine)
                .NotEmpty()
                .MaximumLength(250)
                .WithMessage("AddressLine Is required.");

            RuleFor(x=>x.PhoneNumber)
                .NotEmpty()
                .MaximumLength(20)
                .WithMessage("PhoneNumber Is required.");

            RuleFor(x=>x.City)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("City Is required.");

        }
    }
}
