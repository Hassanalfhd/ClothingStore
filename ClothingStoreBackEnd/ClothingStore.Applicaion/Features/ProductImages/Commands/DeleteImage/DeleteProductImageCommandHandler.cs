using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Application.Interfaces.Services;
using ClothingStore.Domain.Common;
using ClothingStore.Domain.Entities;
using MediatR;

namespace ClothingStore.Application.Features.ProductImages.Commands.DeleteImage;

public class DeleteProductImageCommandHandler
    : IRequestHandler<DeleteProductImageCommand, Result<Guid>>
{
    private readonly IProductImageRepo _productImageRepo;
    private readonly IImageStorageService _storageService;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductImageCommandHandler(
        IProductImageRepo productImageRepo,
        IImageStorageService storageService,
        IUnitOfWork unitOfWork)
    {
        _productImageRepo = productImageRepo;
        _storageService = storageService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        DeleteProductImageCommand request,
        CancellationToken cancellationToken)
    {
        var image = await _productImageRepo
            .GetByIdAsync(
                request.ProductImageId,
                cancellationToken);


        if (image is null)
            return Result<Guid>.Failure("Image not found.");

        // Delete physical file
        if (!string.IsNullOrWhiteSpace(image.ImageUrl))
        {
            await _storageService.DeleteAsync(
                image.ImageUrl,
                cancellationToken);
        }

        bool wasPrimary = image.IsPrimary;

        long? productId = image.ProductId;
        long? variantId = image.ProductVariantId;

        await _productImageRepo.DeleteAsync(image);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Assign another primary image automatically
        if (wasPrimary)
        {
            ProductImage? firstImage = null;

            if (productId.HasValue)
            {
                firstImage = await _productImageRepo
                    .GetFirstProductImageAsync(
                        productId.Value,
                        cancellationToken);
            }
            else if (variantId.HasValue)
            {
                firstImage = await _productImageRepo
                    .GetFirstVariantImageAsync(
                        variantId.Value,
                        cancellationToken);
            }

            if (firstImage is not null)
            {
                firstImage.SetPrimary(true);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }

        return Result<Guid>.Success(request.ProductImageId);
    }
}