using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Application.Features.Catalog.Cart.Dtos
{
    public class CartItemDto
    {
        public long VariantId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public decimal UnitPrice { get; set; }

        public string Currency { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public decimal LineTotal { get; set; }
    }
}
