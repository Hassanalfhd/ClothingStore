using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Entities;

namespace ClothingStore.Infrastructure.Persistence.Repositories
{
    public class ProductRepo: IProductRepo
    {

        private readonly ApplicationDbContext _context;

        public ProductRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Product product, CancellationToken cancellationToken) => _context.AddAsync(product,  cancellationToken);
    

    }
}
