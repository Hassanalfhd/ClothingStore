using ClothingStore.Application.Features.Products.Queries.GetProductById;
using ClothingStore.Application.Features.Products.Queries.GetProducts;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Common;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Enums;
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

    public async Task<PagedResult<ProductListDto>> GetProductsAsync(
        string? search,
        long? categoryId,
        ProductStatus? status,
        decimal? minPrice,
        decimal? maxPrice,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken)
    {

        var query = _context.Products
            .AsNoTracking()
            .AsQueryable();

        // Filtering 
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(x => x.Name.Contains(search));
        }


        if (categoryId.HasValue)
        {
            query = query.Where(x => x.CategoryId == categoryId);
        }

        if (status.HasValue)
        {
            query = query.Where(x => x.Status == status);
        }

        if (minPrice.HasValue)
        {
            query = query.Where(x => x.BasePrice.Amount >= minPrice);
        }

        if (maxPrice.HasValue)
        {
            query = query.Where(x => x.BasePrice.Amount <= maxPrice);
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .Select(x => new ProductListDto
        {
            PublicId = x.PublicId,
            Name = x.Name,
            Price = x.BasePrice.Amount,
            Currency = x.BasePrice.Currency,
            Status = x.Status.ToString(),
            CategoryName = x.Category.Name,

            TotalStock = x.Variants.Sum(v => v.StockQuantity),

            ImageUrl = x.Images
                .Where(i => i.IsPrimary && i.Processed == Processed.Completed)
                .Select(i => i.ImageUrl)
                .FirstOrDefault()
        })
        .ToListAsync(cancellationToken);


        return new PagedResult<ProductListDto>()
        {
            Items = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount
        };
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
                Images = x.Images.Where(i=>i.Processed == Processed.Completed)
                .Select(i=>new ProductImageDto
                {
                    PublicId = i.PublicId,
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
                    Images = v.Images.Where(i => i.Processed == Processed.Completed)
                        .Select(i => new ProductImageDto
                        {
                            PublicId =  i.PublicId,
                            ImageUrl = i.ImageUrl,
                            DisplayOrder = i.DisplayOrder,
                            IsPrimary = i.IsPrimary,
                        }).ToList(),
                    
                }).ToList(),
                
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}
