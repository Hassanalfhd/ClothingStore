using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Application.Features.Catalog.Cart.Dtos
{
    public class ManageCartItemQuantityDto
    {
        public Guid UserId { get; set; }
        public Guid CartItemPublicId { get; set; }
    }
}
