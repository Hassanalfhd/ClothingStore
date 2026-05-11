using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Common;
using MediatR;

namespace ClothingStore.Application.Features.Products.Queries.GetProductById
{
    public sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductDetailsDto>>
    {

        private readonly IProductReadRepos _repository;

        public GetProductByIdQueryHandler(IProductReadRepos productRepo)
        {
            _repository = productRepo;
        }


        public async Task<Result<ProductDetailsDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {

            var product = await _repository
             .GetDetailsByPublicIdAsync(
                 request.PublicId,
                 cancellationToken);


            if (product is null)
                return Result<ProductDetailsDto>
                    .Failure("Product was not found.");

            return Result<ProductDetailsDto>.Success(product);
        }
    }
}
