using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.Features.Catalog.Cart.Dtos;
using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.Interfaces.Repositories
{
    public interface ICartRepo
    {
        Task<Cart?> GetByUserIdAsync(long userId, CancellationToken cancellationToken);

        Task<Cart?> GetByPublicIdAsync(Guid publicId, CancellationToken cancellationToken);

        Task<bool>ClearCartAsync(Guid PublicId, CancellationToken cancellationToken);
        Task AddAsync(Cart cart, CancellationToken cancellationToken);

        Task<CartDto?> GetCartAsync(long userId, CancellationToken cancellationToken);
        Task<bool> UserHasActiveCartAsync(long userId, CancellationToken cancellationToken);
    }
}
