using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Common;
using MediatR;

namespace ClothingStore.Application.Features.Products.Commands.ToggleActivateProduct
{
    public class ToggleActivateCommandHandler: IRequestHandler<ToggleActivateCommand, Result>
    {

        private readonly IProductRepo _productRepo;
        private readonly IUnitOfWork _unitOfWork;
        public ToggleActivateCommandHandler(IProductRepo productRepo, IUnitOfWork unitOfWork)
        {
            _productRepo = productRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(ToggleActivateCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepo.GetByIdAsync(request.PublicId, cancellationToken);

            if (product == null)
                return Result.Failure($"Product with id {request.PublicId} not found.");

            product.ToggleActivate();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

    }
}
