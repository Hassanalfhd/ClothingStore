using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Entities;

namespace ClothingStore.Infrastructure.Persistence.Repositories
{
    public class ProductVariantRepo : IProductVariantRepo
    {

        private readonly ApplicationDbContext _context;

        public ProductVariantRepo(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task AddAsync(ProductVariant productVariant, CancellationToken cancellationToken) => await _context.ProductsVariant.AddAsync(productVariant, cancellationToken);

        /*
            -- Add Product --> public id -->  image and variant
            -- Add Variant --> public id for image
         */



    }
}
