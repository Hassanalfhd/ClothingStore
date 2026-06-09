using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.Interfaces.Repositories
{
    public interface IOrderRepo
    {
        Task AddAsync(
     Order order,
     CancellationToken cancellationToken = default);

        Task<Order?> GetByPublicIdAsync(
            Guid publicId,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Order>> GetByUserIdAsync(
      long userId,
      CancellationToken cancellationToken = default);
    }
}
