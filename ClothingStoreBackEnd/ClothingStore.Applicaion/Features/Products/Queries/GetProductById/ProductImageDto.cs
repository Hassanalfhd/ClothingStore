using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Application.Features.Products.Queries.GetProductById
{
    public sealed class ProductImageDto
    {
        public string ImageUrl { get; set; } = default;

        public bool IsPrimary { get; set; }
        public int DisplayOrder { get; set;  }
    }
}
