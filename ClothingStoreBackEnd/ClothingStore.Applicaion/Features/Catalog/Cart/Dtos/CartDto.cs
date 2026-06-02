using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Application.Features.Catalog.Cart.Dtos
{
    public class CartDto
    {
        public Guid CartId { get; set; }

        public int TotalItems { get; set; }

        public decimal SubTotal { get; set; }

        public List<CartItemDto> Items { get; set; } = new();
    }
}
