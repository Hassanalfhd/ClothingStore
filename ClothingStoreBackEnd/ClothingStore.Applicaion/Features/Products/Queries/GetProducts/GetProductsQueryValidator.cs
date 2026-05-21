using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ClothingStore.Application.Features.Products.Queries.GetProducts
{
    public class GetProductsQueryValidator:AbstractValidator<GetProductsQuery>
    {
        public GetProductsQueryValidator()
        {
            RuleFor(x => x.Search)
                .MaximumLength(200)
                .When(x=> !string.IsNullOrWhiteSpace(x.Search));

            RuleFor(x => x.PageNumber)
                .GreaterThan(0); 

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 50);

            RuleFor(x => x.MinPrice)
                .GreaterThan(0)
                .When(x => x.MinPrice.HasValue);

            RuleFor(x => x.MaxPrice)
                .GreaterThan(0)
                .When(x => x.MaxPrice.HasValue);

            RuleFor(x => x)
            .Must(x =>
                !x.MinPrice.HasValue ||
                !x.MaxPrice.HasValue ||
                x.MinPrice <= x.MaxPrice)
            .WithMessage(
                "MinPrice cannot be greater than MaxPrice.");

            RuleForEach(x => x.Specifications)
                .MaximumLength(200);

            RuleFor(x => x.SortBy)
                .IsInEnum();

        }
    }
}
