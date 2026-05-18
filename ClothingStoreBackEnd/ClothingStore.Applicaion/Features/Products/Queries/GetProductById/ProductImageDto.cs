using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Application.Features.Products.Queries.GetProductById
{
    public sealed class ProductImageDto
    {
        public Guid PublicId { get; set; }
        public string? ImageUrl {  get; set; }
        public bool IsPrimary { get; set; }
        public int DisplayOrder { get; set;  }
    }
}
