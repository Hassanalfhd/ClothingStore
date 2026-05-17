using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Common;
using ClothingStore.Domain.Entities;
using MediatR;

namespace ClothingStore.Application.Features.ProductImages.Commands.SetPrimaryImage;

public class SetPrimaryProductImageCommandHandler
    : IRequestHandler<SetPrimaryProductImageCommand, Result<Guid>>
{
    private readonly IProductImageRepo _productImageRepo;
    private readonly IUnitOfWork _unitOfWork;

    public SetPrimaryProductImageCommandHandler(
        IProductImageRepo productImageRepo,
        IUnitOfWork unitOfWork)
    {
        _productImageRepo = productImageRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        SetPrimaryProductImageCommand request,
        CancellationToken cancellationToken)
    {
        var image = await _productImageRepo
            .GetByIdAsync(
                request.ProductImageId,
                cancellationToken);

        if (image is null)
            return Result<Guid>.Failure("Image not found.");

        List<ProductImage> images;

        // Product images
        if (image.ProductId.HasValue)
        {
            images = await _productImageRepo
                .GetByProductIdAsync(
                    image.ProductId.Value,
                    cancellationToken);
        }

        // Variant images
        else
        {
            images = await _productImageRepo
                .GetByVariantIdAsync(
                    image.ProductVariantId!.Value,
                    cancellationToken);
        }

        // Remove old primary
        foreach (var item in images)
        {
            item.SetPrimary(false);
        }

        // Set new primary
        image.SetPrimary(true);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(image.PublicId);
    }
}