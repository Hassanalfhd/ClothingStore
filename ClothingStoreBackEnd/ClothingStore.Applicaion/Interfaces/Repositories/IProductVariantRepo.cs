using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.Interfaces.Repositories
{
    public interface IProductVariantRepo
    {
        Task AddAsync(ProductVariant productVariant, CancellationToken cancellationToken);

    }
}
