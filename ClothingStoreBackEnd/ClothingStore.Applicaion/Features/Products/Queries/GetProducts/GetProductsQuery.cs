using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Domain.Common;
using ClothingStore.Domain.Enums;
using MediatR;

namespace ClothingStore.Application.Features.Products.Queries.GetProducts
{
    public class GetProductsQuery : IRequest<PagedResult<ProductListDto>>
    {
        public string? Search { get; set; }

        public long? CategoryId { get; set; }

        public ProductStatus? Status { get; set; }

        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public List<string>? Specifications { get; set; }
        public ProductSortBy SortBy { get; set; } = ProductSortBy.Newest;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
}
