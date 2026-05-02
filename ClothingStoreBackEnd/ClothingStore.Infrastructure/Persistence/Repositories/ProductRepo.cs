using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Infrastructure.Persistence.Repositories
{
    public class ProductRepo: IProductRepo
    {

        private readonly ApplicationDbContext _context;

        public ProductRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Product product, CancellationToken cancellationToken) => await _context.AddAsync(product,  cancellationToken);
        public void Update(Product product) => _context.Update(product);

        public async Task<Product?> GetByIdAsync(Guid publicId, CancellationToken cancellationToken) => await _context.Products.FirstOrDefaultAsync(x=>x.PublicId == publicId, cancellationToken);

    }
}
