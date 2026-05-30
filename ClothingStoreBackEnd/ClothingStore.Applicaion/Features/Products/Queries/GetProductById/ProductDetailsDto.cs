
namespace ClothingStore.Application.Features.Products.Queries.GetProductById
{
    public class ProductDetailsDto
    {
        public Guid PublicId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal BasePrice { get; set; }

        public string Currency { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public string CategoryName { get; set; } = string.Empty;
        public string? BrandName { get; set; } = string.Empty;

        public List<ProductImageDto> Images { get; set; } = new();

        public List<ProductVariantDto> Variants { get; set; } = new();

        public Dictionary<string, string> Specifications { get; set; }
            = new();

    }

}
