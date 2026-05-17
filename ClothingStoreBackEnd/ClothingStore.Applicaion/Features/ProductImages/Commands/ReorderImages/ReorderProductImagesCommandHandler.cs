using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Common;
using MediatR;

namespace ClothingStore.Application.Features.ProductImages.Commands.ReorderImages;

public class ReorderProductImagesCommandHandler
    : IRequestHandler<ReorderProductImagesCommand, Result<bool>>
{
    private readonly IProductImageRepo _productImageRepo;
    private readonly IUnitOfWork _unitOfWork;

    public ReorderProductImagesCommandHandler(
        IProductImageRepo productImageRepo,
        IUnitOfWork unitOfWork)
    {
        _productImageRepo = productImageRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(
        ReorderProductImagesCommand request,
        CancellationToken cancellationToken)
    {
        var publicIds = request.Images
            .Select(x => x.ProductImageId)
            .ToList();

        var images = await _productImageRepo
            .GetByPublicIdsAsync(
                publicIds,
                cancellationToken);


        if (images.Count != request.Images.Count)
        {
            return Result<bool>.Failure(
                "Some images were not found.");
        }

        foreach (var image in images)
        {
            var newOrder = request.Images
                .First(x => x.ProductImageId == image.PublicId)
                .DisplayOrder;

            image.SetOrder(newOrder);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}