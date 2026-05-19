using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Application.Features.Catalog.Brand.Dtos
{
    public class BrandDto
    {
        public Guid PublicId { get; set; }
        public string Slug { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description{ get; set; }
        public string? LogoUrl { get; set; }

    }
}
