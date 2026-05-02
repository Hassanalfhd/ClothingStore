using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ClothingStore.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductValidator: AbstractValidator<UpdateProductCommand>
    {

        public UpdateProductValidator()
        {
            RuleFor(x => x.Name).MaximumLength(200).NotEmpty().NotNull();
            RuleFor(x => x.Description).MaximumLength(200).Null();
            RuleFor(x => x.CategoryId).NotNull().NotEmpty();
            RuleFor(x => x.CreatedBy).NotNull().NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.Currency).NotEmpty();
        }
    }
}
