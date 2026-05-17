using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.Interfaces.Repositories;

public interface IProductImageRepo
{
    Task AddAsync(ProductImage image, CancellationToken cancellationToken);
    Task<ProductImage?> GetByIdAsync(Guid publicId, CancellationToken cancellationToken);
    Task<ProductImage?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<List<ProductImage>> GetByVariantIdAsync(long variantId, CancellationToken cancellationToken);
    Task DeleteAsync(ProductImage image);
    Task<List<ProductImage>> GetByProductIdAsync(
    long productId,
    CancellationToken cancellationToken);

    Task<ProductImage?> GetFirstVariantImageAsync(long? variantId, CancellationToken cancellationToken);
    Task<ProductImage?> GetFirstProductImageAsync(long? productId, CancellationToken cancellationToken);
    Task<List<ProductImage>> GetByPublicIdsAsync(
    List<Guid> publicIds,
    CancellationToken cancellationToken);
}