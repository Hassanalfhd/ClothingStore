using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Application.Features.Products.Queries.GetProductById
{
    public class  ProductImageWithMultipleSizeDto
    {
        public string ThumbnailUrl { get; set; } = default!;

        public string MediumUrl { get; set; } = default!;

        public string OriginalUrl { get; set; } = default!;

        public bool IsPrimary { get; set; }
        public int DisplayOrder { get; set; }
    }
}
