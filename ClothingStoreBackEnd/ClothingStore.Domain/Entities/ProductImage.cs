using ClothingStore.Domain.Enums;

namespace ClothingStore.Domain.Entities
{
    public class ProductImage : EntityBased
    {

        private ProductImage() { }

        public ProductImage(
            long? productId,
            long? productVariantId,
            string imageUrl,
            int displayOrder)
        {
            ProductId = productId;
            ProductVariantId = productVariantId;

            SetImageUrl(imageUrl);
            SetOrder(displayOrder);
            Processed = Processed.Pending;
            IsPrimary = false;
        }

        public long? ProductId { get; private set; }
        public long? ProductVariantId { get; private set; }
        public string ImageUrl { get; private set; } = string.Empty;

        public bool IsPrimary { get; private set; }

        public int DisplayOrder { get; private set; }

        public Processed Processed { get; private set; }
        
        public Product? Product { get; private set; } = null!;
        public ProductVariant? ProductVariant { get; private set; }


        public void SetImageUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("Image URL cannot be empty.");

            ImageUrl = url.Trim();
        }


        public void MarkAsProcessedFailed()
        {
            Processed = Processed.Failed;
        }

        public void MarkAsProcessing()
        {
            Processed = Processed.Processing;
        }

        public void MarkAsProcessed()
        {
            Processed = Processed.Completed;
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

        public void AssignVariant(long? variantId)
        {
            ProductVariantId = variantId;
        }

    }
}
