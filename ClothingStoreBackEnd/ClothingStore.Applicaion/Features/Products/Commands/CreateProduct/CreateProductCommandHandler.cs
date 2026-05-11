using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Common;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.ValueObjects;
using MediatR;

namespace ClothingStore.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<Guid>>
    {

        private readonly IProductRepo _productRepo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly IUserRepo _userRepo;
        private readonly IBrandRepo _brandRepo;
        private readonly IUnitOfWork _unitOfWork;
        public CreateProductCommandHandler(IProductRepo productRepo, IUnitOfWork unitOfWork, ICategoryRepo categoryRepo, IUserRepo userRepo, IBrandRepo brandRepo)
        {
            _productRepo = productRepo;
            _unitOfWork = unitOfWork;
            _categoryRepo = categoryRepo;
            _userRepo = userRepo;
            _brandRepo = brandRepo;
        }

        public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {

            var CategoryId = await _categoryRepo.GetIdAsync(request.CategoryId);
            var CreatedBy = await _userRepo.GetIdAsync(request.CreatedBy);
            var BrandId = await _brandRepo.GetIdAsync(request.BrandId);


            if (CategoryId == null) return Result<Guid>.Failure($"Category not found.");
            if (CreatedBy == null) return Result<Guid>.Failure($"User not found.");
            if (BrandId == null) return Result<Guid>.Failure($"Brand not found.");


            var newProduct = new Product(
                    request.Name,
                    request.Description,
                    new Money(request.Price, request.Currency),
                    request.IsActive, CreatedBy.Value, CategoryId.Value, BrandId

                );


            await _productRepo.AddAsync(newProduct, cancellationToken);

            await _unitOfWork.SaveChangesAsync();

            return Result<Guid>.Success(newProduct.PublicId);


            }

        }

    }
}
