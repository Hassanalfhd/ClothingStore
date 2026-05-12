using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Common;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.ValueObjects;
using MediatR;

namespace ClothingStore.Application.Features.ProductVariants.Commands.CreateVariant
{
    public class CreateProductVariantCommandHandler:IRequestHandler<CreateProductVariantCommand,Result<Guid>>
    {

        private readonly IProductReadRepos _productRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepo _userRepo;
        private readonly IProductVariantRepo _productVariantRepo;
        private readonly ISizeRepo _sizeRepo;
        private readonly IColorRepo _colorRepo;

        public CreateProductVariantCommandHandler(IProductReadRepos productRepo, IUnitOfWork unitOfWork, IUserRepo userRepo, IProductVariantRepo productVariantRepo, ISizeRepo sizeRepo, IColorRepo colorRepo)
        {
            _productRepo = productRepo;
            _unitOfWork = unitOfWork;
            _userRepo = userRepo;
            _productVariantRepo = productVariantRepo;
            _sizeRepo = sizeRepo;
            _colorRepo = colorRepo;
        }


        public async Task<Result<Guid>> Handle(CreateProductVariantCommand request, CancellationToken cancellationToken)
        {
            var ProductId = await _productRepo.GetProductId(request.ProductId, cancellationToken);
            var ColorId = await _colorRepo.GetIdAsync(request.ColorId);
            var SizeId = await _sizeRepo.GetIdAsync(request.SizeId);
            var CreatedBy = await _userRepo.GetIdAsync(request.CreatedBy);

            if (ProductId== null) return Result<Guid>.Failure($"Product not found.");
            if (ColorId == null) return Result<Guid>.Failure($"Color not found.");
            if (SizeId == null) return Result<Guid>.Failure($"Size not found.");
            if (CreatedBy == null) return Result<Guid>.Failure($"User not found.");


            var money = new Money(request.Price, request.Currency);

            var variant = new ProductVariant(ProductId.Value, ColorId.Value, SizeId.Value, CreatedBy.Value, money, request.StockQuantity, request.SKU);


            await _productVariantRepo.AddAsync(variant, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(variant.PublicId);

        }


    }
}
