using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Application.Features.Carts.Commands.AddToCart
{
    public record AddToCartCommand(
    Guid UserId,
    Guid ProductId,
    Guid? VariantId,
    int Quantity);

}
