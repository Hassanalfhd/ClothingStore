using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Application.Features.Products.Queries.GetProducts
{
    public class ProductListDto
    {
        public Guid PublicId { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string Currency { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public string Status { get; set; } = string.Empty;

        public string CategoryName { get; set; } = string.Empty;

        public int TotalStock { get; set; }

    }
}
