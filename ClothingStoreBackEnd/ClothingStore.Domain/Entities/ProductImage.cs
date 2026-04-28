namespace ClothingStore.Domain.Entities
{
    public class ProductImage : EntityBased
    {

        private ProductImage() { }

        public long ProductId { get; private set; }
        public long ProductVariantId { get; private set; }
        public string ImageUrl { get; private set; } = string.Empty;

        public bool IsPrimary { get; private set; }

        public int DisplayOrder { get; private set; }

        public bool IsProcessed { get; private set; }

        public Product Product { get; private set; } = null!;
        public ProductVariant? ProductVariant { get; private set; }

        public void SetImageUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("Image URL cannot be empty.");

            ImageUrl = url.Trim();
        }

        public void MarkAsProcessed()
        {
            IsProcessed = true;
        }

        public void SetPrimary(bool isPrimary)
        {
            IsPrimary = isPrimary;
        }

        public void SetOrder(int order)
        {
            if (order < 0)
                throw new ArgumentOutOfRangeException(nameof(order));

            DisplayOrder = order;
        }

    }
}
