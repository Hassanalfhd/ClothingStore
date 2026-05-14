using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ClothingStore.Application.Features.ProductImages.Commands.CreateImage
{
    public  class CreateProductImageValidator:AbstractValidator<CreateProductImageCommand>
    {
        public CreateProductImageValidator()
        {
            RuleFor(x => x.File)
                .NotNull()
                .WithMessage("Image file is required.");

            RuleFor(x => x)
                .Must(x =>
                    (x.ProductId.HasValue && !x.ProductVariantId.HasValue) ||
                    (!x.ProductId.HasValue && x.ProductVariantId.HasValue))
                .WithMessage("Image must belong to either Product OR ProductVariant, not both.");
        }



    }

}
