using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ClothingStore.Application.Features.ProductVariants.Commands.UpdateVariant
{
    public class UpdateVariantValidator:AbstractValidator<UpdateVariantCommand>
    {
        public UpdateVariantValidator()
        {
            RuleFor(x => x.SKU).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.StockQuantity).GreaterThan(0);
            RuleFor(x => x.Currency).NotNull().NotEmpty();

        }
    }
}
