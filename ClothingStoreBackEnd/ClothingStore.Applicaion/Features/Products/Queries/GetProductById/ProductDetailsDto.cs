using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Application.Features.Products.Queries.GetProductById
{
    public class ProductDetailsDto
    {
        public Guid PublicId { get; set; }

        public string Name { get; set; } = default!;
        public string PrimaryImageUrl { get; set; } = default!;

        public string? Description { get; set; }

        public decimal BasePrice { get; set; }

        public string Currency { get; set; } = default!;

        public string CategoryName { get; set; } = default!;

        public bool IsActive { get; set; }

        public List<ProductImageDto> Images { get; set; } = [];
        public List<ProductVariantDto> Variants { get; set; } = [];
    }
}
