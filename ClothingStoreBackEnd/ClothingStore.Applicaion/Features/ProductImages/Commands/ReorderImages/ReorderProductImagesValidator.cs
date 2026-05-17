using FluentValidation;

namespace ClothingStore.Application.Features.ProductImages.Commands.ReorderImages;

public class ReorderProductImagesValidator
    : AbstractValidator<ReorderProductImagesCommand>
{
    public ReorderProductImagesValidator()
    {
        RuleFor(x => x.Images)
            .NotEmpty();

        RuleForEach(x => x.Images)
            .ChildRules(image =>
            {
                image.RuleFor(x => x.ProductImageId)
                    .NotEmpty();

                image.RuleFor(x => x.DisplayOrder)
                    .GreaterThanOrEqualTo(0);
            });
    }
}