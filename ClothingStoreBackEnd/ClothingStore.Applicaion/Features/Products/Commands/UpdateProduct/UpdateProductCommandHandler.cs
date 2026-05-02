using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Common;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.ValueObjects;
using MediatR;

namespace ClothingStore.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler: IRequestHandler<UpdateProductCommand, Result>
    {
        private readonly IProductRepo _productRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepo _categoryRepo;
        private readonly IUserRepo _userRepo;

        public UpdateProductCommandHandler(IProductRepo productRepo, IUnitOfWork unitOfWork, ICategoryRepo categoryRepo, IUserRepo userRepo)
        {
            _productRepo = productRepo;
            _unitOfWork = unitOfWork;
            _categoryRepo = categoryRepo;
            _userRepo = userRepo;
        }


        public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken = default)
        {
            
            var product = await _productRepo.GetByIdAsync(request.PublicId, cancellationToken);

            if (product == null) return Result.Failure($"Error:Product with id {request.PublicId} is not found.");

            var CategoryId = await _categoryRepo.GetIdAsync(request.CategoryId);
            var CreatedBy = await _userRepo.GetIdAsync(request.CreatedBy);

            if (CategoryId == null) return Result.Failure($"Category with {CategoryId} is not found.");
            if (CreatedBy == null) return Result.Failure($"User with {CreatedBy} is not found.");


            var newProduct = new Product(
                    request.Name,
                    request.Description,
                    new Money(request.Price, request.Currency),
                    product.IsActive,
                    CreatedBy.Value, CategoryId.Value
                );

            
            product.UpdateProduct( newProduct );

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();

        }

    }
}
