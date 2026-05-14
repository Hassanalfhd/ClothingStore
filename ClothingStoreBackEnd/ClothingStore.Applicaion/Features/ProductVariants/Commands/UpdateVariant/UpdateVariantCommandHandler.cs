using ClothingStore.Application.Features.ProductVariants.Commands.CreateVariant;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Common;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.ValueObjects;
using MediatR;

namespace ClothingStore.Application.Features.ProductVariants.Commands.UpdateVariant
{
    public class UpdateVariantCommandHandler: IRequestHandler<UpdateVariantCommand, Result>
    {
        private readonly IProductReadRepos _productRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepo _userRepo;
        private readonly IProductVariantRepo _productVariantRepo;
        private readonly ISizeRepo _sizeRepo;
        private readonly IColorRepo _colorRepo;

        public UpdateVariantCommandHandler(IProductReadRepos productRepo, IUnitOfWork unitOfWork, IUserRepo userRepo, IProductVariantRepo productVariantRepo, ISizeRepo sizeRepo, IColorRepo colorRepo)
        {
            _productRepo = productRepo;
            _unitOfWork = unitOfWork;
            _userRepo = userRepo;
            _productVariantRepo = productVariantRepo;
            _sizeRepo = sizeRepo;
            _colorRepo = colorRepo;
        }


        public async Task<Result> Handle(UpdateVariantCommand request, CancellationToken cancellationToken)
        {
            var variant = await _productVariantRepo.GetByIdAsync(request.VaraintId, cancellationToken);
            var productId = await _productRepo.GetProductId(request.ProductId, cancellationToken);
            var colorId = await _colorRepo.GetIdAsync(request.ColorId);
            var sizeId = await _sizeRepo.GetIdAsync(request.SizeId);
            var createdBy = await _userRepo.GetIdAsync(request.CreatedBy);


            if (variant == null) return Result.Failure($"Product Variant not found.");
            if (productId == null) return Result.Failure($"Product not found.");
            if (colorId == null) return Result.Failure($"Color not found.");
            if (sizeId == null) return Result<Guid>.Failure($"Size not found.");
            if (createdBy == null) return Result.Failure($"User not found.");


            var money = new Money(request.Price, request.Currency);
            var newVariant = new ProductVariant(productId.Value, colorId.Value, sizeId.Value, createdBy.Value, money, request.StockQuantity, request.SKU);

            variant.UpdateVariant(newVariant);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();

        }


    }
}
