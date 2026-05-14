using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Application.Interfaces.Services;
using ClothingStore.Domain.Common;
using ClothingStore.Domain.Entities;
using MediatR;

namespace ClothingStore.Application.Features.ProductImages.Commands.CreateImage
{
    public class CreateProductImageCommandHandler: IRequestHandler<CreateProductImageCommand, Result<Guid>>
    {

        private readonly IProductImageRepo _productImageRepo;
        private readonly IImageStorageService _storageService;
        private readonly IBackgroundTaskQueue _queue;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductImageCommandHandler(IProductImageRepo productImageRepo, IImageStorageService storageService, IBackgroundTaskQueue queue, IUnitOfWork unitOfWork)
        {
            _productImageRepo = productImageRepo;
            _storageService = storageService;
            _queue = queue;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateProductImageCommand request, CancellationToken cancellationToken)
        {
            var tempResult = await _storageService.SaveTempAsync(request.File, cancellationToken);

            //var ProductId = await _porductRepo.GetProductId(request.ProductId, cancellationToken);
            //var ProductVariantId = await _colorRepo.GetIdAsync(request.ColorId);

            //if (ProductId == null) return Result<Guid>.Failure($"Product not found.");


            //var image = new ProductImage(
            //     request.ProductId,
            //     request.ProductVariantId,
            //     tempResult.FileName,
            //     request.IsPrimary,
            //     request.DisplayOrder);
        
        }





    }
}
