using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Common;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.ValueObjects;
using MediatR;

namespace ClothingStore.Application.Features.Products.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<Guid>>
    {

        private readonly IProductRepo _productRepo;
        private readonly IUnitOfWork _unitOfWork;
        public CreateProductCommandHandler(IProductRepo productRepo, IUnitOfWork unitOfWork)
        {
            _productRepo = productRepo;
            _unitOfWork = unitOfWork;
        }


        public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var newProduct = new Product(
                    request.Name,
                    request.Description,
                    new Money(request.Price, request.Currency),
                    request.IsActive, request.CreatedBy, request.CategoryId
                );


            await _productRepo.AddAsync(newProduct, cancellationToken);

            await _unitOfWork.SaveChangesAsync();


            return Result<Guid>.Success(newProduct.PublicId);

        }

    }
}
