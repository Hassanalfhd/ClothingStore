using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.Interfaces.Repositories
{
    public interface IProductVariantRepo
    {
        Task AddAsync(ProductVariant productVariant, CancellationToken cancellationToken);
        Task<long?> GetProductVariantId(Guid PublicId, CancellationToken cancellationToken);

        Task<ProductVariant?> GetByIdAsync(Guid PublicId, CancellationToken cancellationToken);
    }
}
