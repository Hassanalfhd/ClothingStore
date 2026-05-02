using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ClothingStore.Application.Features.Products.CreateProduct
{
    public  class CreateProductValidator: AbstractValidator<CreateProductCommand>
    {

        public CreateProductValidator()
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
