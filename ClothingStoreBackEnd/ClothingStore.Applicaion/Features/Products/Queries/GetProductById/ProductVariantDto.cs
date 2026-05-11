using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Application.Features.Products.Queries.GetProductById
{
    public sealed class ProductVariantDto
    {
        public Guid PublicId { get; set; }
        public string SKU { get; set; }
        public string Color { get; set; } = default!;
        public string Size { get; set; } = default!;
        public int StockQuantity { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }

        public List<ProductImageDto> Images { get; set; } = [];
    }
}
