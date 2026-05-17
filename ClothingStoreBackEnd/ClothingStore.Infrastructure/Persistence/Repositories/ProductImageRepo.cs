using global::ClothingStore.Application.Interfaces.Repositories;
using global::ClothingStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Infrastructure.Persistence.Repositories;

public class ProductImageRepo : IProductImageRepo
{
    private readonly ApplicationDbContext _context;

    public ProductImageRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(ProductImage image, CancellationToken cancellationToken)
    {
        await _context.ProductImages.AddAsync(image, cancellationToken);
    }
    public async Task<ProductImage?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _context.ProductImages
            .FirstOrDefaultAsync(x => x.Id== id, cancellationToken);
    }

    public async Task<ProductImage?> GetByIdAsync(Guid publicId, CancellationToken cancellationToken)
    {
        return await _context.ProductImages
            .FirstOrDefaultAsync(x => x.PublicId == publicId, cancellationToken);
    }

    public async Task<List<ProductImage>> GetByVariantIdAsync(long variantId, CancellationToken cancellationToken)
    {
        return await _context.ProductImages
            .Where(x => x.ProductVariantId == variantId)
            .OrderBy(x => x.DisplayOrder)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<ProductImage>> GetByProductIdAsync(long productId, CancellationToken cancellationToken)
    {
        return await _context.ProductImages
            .Where(x => x.ProductId == productId)
            .OrderBy(x => x.DisplayOrder)
            .ToListAsync(cancellationToken);
    }

    public async Task<ProductImage?> GetFirstVariantImageAsync(long? variantId, CancellationToken cancellationToken)
    {
        return await _context.ProductImages
            .Where(x => x.ProductVariantId == variantId)
            .OrderBy(x => x.DisplayOrder)
            .FirstOrDefaultAsync(cancellationToken);
    }
    public async Task<ProductImage?> GetFirstProductImageAsync(long? productId, CancellationToken cancellationToken)
    {
        return await _context.ProductImages
            .Where(x => x.ProductId== productId)
            .OrderBy(x=>x.DisplayOrder)
            .FirstOrDefaultAsync(cancellationToken);

    }
    
    public async Task DeleteAsync(ProductImage image)
    {
        _context.ProductImages.Remove(image);

        await Task.CompletedTask;
    }

    public async Task<List<ProductImage>> GetByPublicIdsAsync(
      List<Guid> publicIds,
      CancellationToken cancellationToken)
    {
        if (publicIds is null || publicIds.Count == 0)
            return [];

        var ids = publicIds
            .Distinct()
            .ToList();

        return await _context.ProductImages
            .Where(x => ids.Contains(x.PublicId))
            .ToListAsync(cancellationToken);
    }


}
