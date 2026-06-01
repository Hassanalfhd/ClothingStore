using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ClothingStore.Application.DTOs;
using ClothingStore.Application.Features.Products.Queries.GetProductById;
using ClothingStore.Application.Features.Products.Queries.GetProducts;
using ClothingStore.Domain.Common;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Enums;

namespace ClothingStore.Application.Interfaces.Repositories
{
    public interface IProductReadRepos
    {
        Task<ProductDetailsDto?> GetDetailsByPublicIdAsync(
            Guid publicId,
            CancellationToken cancellationToken);

        Task<PagedResult<ProductListDto>> GetProductsAsync(
        string? search,
        long? categoryId,
        ProductStatus? status,
        decimal? minPrice,
        decimal? maxPrice,
        int pageNumber,
        int pageSize,
        List<string>? specifications,
        ProductSortBy sortBy,
        CancellationToken cancellationToken);

        Task<ProductDto?> GetByIdAsync(Guid publicId, CancellationToken cancellationToken);
        Task<long?> GetProductId(Guid PublicId, CancellationToken cancellationToken);
    }
}
