using System;

namespace ClothingStore.Application.Features.Catalog.Brand.Dtos
{
    public class CreateBrandDto
    {
        public string Slug { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? LogoUrl { get; set; }
    }
}
