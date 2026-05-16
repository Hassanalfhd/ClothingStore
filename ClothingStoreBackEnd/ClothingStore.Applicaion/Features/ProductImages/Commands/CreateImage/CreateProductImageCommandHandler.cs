using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Application.Interfaces.Services;
using ClothingStore.Domain.Common;
using ClothingStore.Domain.Entities;
using MediatR;

namespace ClothingStore.Application.Features.ProductImages.Commands.CreateImage
{
    public class CreateProductImageCommandHandler : IRequestHandler<CreateProductImageCommand, Result<Guid>>
    {

        private readonly IProductImageRepo _productImageRepo;
        private readonly IProductVariantRepo _variantRepo;
        private readonly IProductReadRepos _productReadRepos;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageStorageService _storageService;
        private readonly IBackgroundTaskQueue _queue;


        public CreateProductImageCommandHandler(IProductImageRepo productImageRepo,
           IProductReadRepos productReadRepos,
            IProductVariantRepo variantRepo, IUnitOfWork unitOfWork,
            IImageStorageService storageService, IBackgroundTaskQueue queue)
        {
            _productImageRepo = productImageRepo;
            _productReadRepos = productReadRepos;
            _variantRepo = variantRepo;
            _unitOfWork = unitOfWork;
            _storageService = storageService;
            _queue = queue;
        }


        public async Task<Result<Guid>> Handle(CreateProductImageCommand request, CancellationToken cancellationToken)
        {
            var tempResult = await _storageService.SaveTempAsync(request.File, cancellationToken);
            long? productVariantId = null;
            long? productId = null;

            if (request.ProductVariantId.HasValue)
            {
                productVariantId = await _variantRepo.GetProductVariantId(request.ProductVariantId.Value, cancellationToken);

                if (productVariantId == null)
                    return Result<Guid>.Failure("Product variant not found.");

            }

            if (request.ProductId.HasValue)
            {
                productId = await _productReadRepos.GetProductId(request.ProductId.Value, cancellationToken);
                if (productId == null)
                    return Result<Guid>.Failure("Product not found.");
            }

            
            var image = new ProductImage(productId, productVariantId, tempResult.FileName, request.IsPrimary, request.DisplayOrder);
            
            await _productImageRepo.AddAsync(image, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _queue.QueueAsync(new DTOs.ProcessProductImageJob
            {
                ProductImageId = image.Id,
                TempFilePath = tempResult.TempFilePath,
                FileName = tempResult.FileName,
            });



            return Result<Guid>.Success(image.PublicId);

        }
    }
}


