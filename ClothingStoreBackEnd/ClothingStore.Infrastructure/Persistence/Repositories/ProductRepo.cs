using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Infrastructure.Persistence.Repositories
{
    public class ProductRepo : IProductRepo
    {

        private readonly ApplicationDbContext _context;

        public ProductRepo(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task AddAsync(Product product, CancellationToken cancellationToken) => await _context.AddAsync(product, cancellationToken);

        public async Task<Product?> GetByIdAsync(Guid publicId, CancellationToken cancellationToken) => await _context.Products.FirstOrDefaultAsync(x => x.PublicId == publicId, cancellationToken);


    }
}
