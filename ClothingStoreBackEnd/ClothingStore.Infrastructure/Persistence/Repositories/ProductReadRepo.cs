using ClothingStore.Application.Features.Products.Queries.GetProductById;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public sealed class ProductReadRepo : IProductReadRepos
{
    private readonly ApplicationDbContext _context;

    public ProductReadRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<long?> GetProductId(Guid PublicId, CancellationToken cancellationToken)
    {
        var result = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x=>x.PublicId == PublicId, cancellationToken);

        return result == null ? null : result.Id;


    }

    public async Task<ProductDetailsDto?> GetDetailsByPublicIdAsync(
        Guid publicId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .AsNoTracking()
            .AsSplitQuery()
            .Where(x => x.PublicId == publicId)
            .Select(x => new ProductDetailsDto
            {
                PublicId = x.PublicId,
                Name = x.Name,
                Description = x.Description,
                BasePrice = x.BasePrice.Amount,
                Currency = x.BasePrice.Currency,
                Images = x.Images.Where(i=>i.IsProcessed == true)
                .Select(i=>new ProductImageDto
                {
                    ImageUrl = i.ImageUrl,
                    DisplayOrder = i.DisplayOrder,
                    IsPrimary = i.IsPrimary,
                }).ToList(),
                CategoryName = x.Category.Name,
                IsActive = x.IsActive,

                Variants = x.Variants.Select(v => new ProductVariantDto
                {
                    PublicId = v.PublicId,
                    Color = v.Color.Name,
                    Price = v.Money.Amount,
                    Currency = v.Money.Currency,
                    Size = v.Size.Name,
                    SKU = v.SKU,
                    StockQuantity = v.StockQuantity,
                    Images = x.Images.Where(i => i.IsProcessed!= true)
                        .Select(i => new ProductImageDto
                        {
                            ImageUrl = i.ImageUrl,
                            DisplayOrder = i.DisplayOrder,
                            IsPrimary = i.IsPrimary,
                        }).ToList(),
                    
                }).ToList(),
                
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}
