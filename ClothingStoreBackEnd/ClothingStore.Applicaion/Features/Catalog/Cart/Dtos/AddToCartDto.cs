using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Application.Features.Catalog.Cart.Dtos
{
    public record AddToCartDto(Guid? UserId, Guid ProductId, Guid VariantId, int Quantity);
}
