using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.Interfaces.Repositories
{
    public interface IProductRepo
    {
        Task AddAsync(Product product, CancellationToken cancellationToken);
        Task<Product?> GetByIdAsync(Guid publicId, CancellationToken cancellationToken);
        void Update(Product product);



    }
}
