using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.Features.Catalog.Cart.Dtos;
using ClothingStore.Domain.Common;

namespace ClothingStore.Application.Interfaces.Services
{
    public interface ICartService
    {

        Task<Result> AddToCart(AddToCartDto cartDto, CancellationToken cancellationToken= default);
        Task<Result> RemoveFromCart(ManageCartItemQuantityDto dto, CancellationToken cancellationToken);

        Task<Result> IncreaseQuantity(
     ManageCartItemQuantityDto dto,
     CancellationToken cancellationToken);
        Task<Result> DecreaseQuantity(
    ManageCartItemQuantityDto dto,
    CancellationToken cancellationToken);

    }
}
