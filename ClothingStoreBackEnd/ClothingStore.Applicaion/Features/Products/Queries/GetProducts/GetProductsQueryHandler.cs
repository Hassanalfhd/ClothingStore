using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Common;
using MediatR;

namespace ClothingStore.Application.Features.Products.Queries.GetProducts
{
    public class GetProductsQueryHandler: IRequestHandler<GetProductsQuery, PagedResult<ProductListDto>>
    {

        private readonly IProductReadRepos _productReadRepos;
        public GetProductsQueryHandler(IProductReadRepos productReadRepos)
        {
            _productReadRepos = productReadRepos;
        }



        public async Task<PagedResult<ProductListDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {

            return await _productReadRepos.GetProductsAsync(
                request.Search,
                request.CategoryId,
                request.Status,
                request.MinPrice, 
                request.MaxPrice,
                request.PageNumber,
                request.PageSize, 
                request.Specifications,
                request.SortBy,
                cancellationToken);

        }

    }
}
