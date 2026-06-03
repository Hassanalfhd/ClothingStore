using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.DTOs;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Infrastructure.Persistence.Repositories
{
    public class ProductVariantRepo : IProductVariantRepo
    {

        private readonly ApplicationDbContext _context;

        public ProductVariantRepo(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<long?> GetProductVariantId(Guid PublicId, CancellationToken cancellationToken)
        {
            var result = await _context.ProductsVariant.AsNoTracking().FirstOrDefaultAsync(x => x.PublicId == PublicId, cancellationToken);
            return result == null ? null : result.Id;
        }

        public async Task AddAsync(ProductVariant productVariant, CancellationToken cancellationToken) => await _context.ProductsVariant.AddAsync(productVariant, cancellationToken);

        public async Task<ProductVariant?> GetByIdAsync(Guid PublicId, CancellationToken cancellationToken) => await _context.ProductsVariant.FirstOrDefaultAsync(x=>x.PublicId == PublicId, cancellationToken);

        public  async Task<ProductVariantCartDto?> GetVariantDtoByIdAsync(Guid PublicId, CancellationToken cancellationToken)
       => await _context.ProductsVariant
                .Where(x => x.PublicId == PublicId)
                .Select(x => new ProductVariantCartDto
                {
                    Id = x.Id,
                    PublicId = x.PublicId,
                    Price = x.Money.Amount,
                    Currency = x.Money.Currency,

                    StockQuantity = x.StockQuantity
                })
                .FirstOrDefaultAsync(cancellationToken);

}
}
