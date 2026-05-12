using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ClothingStore.Application.Features.Products.Queries.GetProductById;

namespace ClothingStore.Application.Interfaces.Repositories
{
    public interface IProductReadRepos
    {
        Task<ProductDetailsDto?> GetDetailsByPublicIdAsync(
            Guid publicId,
            CancellationToken cancellationToken);

        Task<long?> GetProductId(Guid PublicId, CancellationToken cancellationToken);
    }
}
