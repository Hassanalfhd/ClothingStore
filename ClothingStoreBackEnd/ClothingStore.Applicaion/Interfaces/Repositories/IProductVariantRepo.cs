using ClothingStore.Application.DTOs;
using ClothingStore.Application.Features.Products.Queries.GetProductById;
using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.Interfaces.Repositories
{
    public interface IProductVariantRepo
    {
        Task AddAsync(ProductVariant productVariant, CancellationToken cancellationToken);
        Task<long?> GetProductVariantId(Guid PublicId, CancellationToken cancellationToken);

        Task<ProductVariant?> GetByIdAsync(Guid PublicId, CancellationToken cancellationToken);
        Task<ProductVariantCartDto?> GetVariantDtoByIdAsync(Guid PublicId, CancellationToken cancellationToken);

    }
}
