using System.Drawing;
using ClothingStore.Domain.ValueObjects;
using static System.Net.Mime.MediaTypeNames;

namespace ClothingStore.Domain.Entities
{
    public class ProductVariant : EntityBased
    {

        private ProductVariant() { }


        public ProductVariant(
        long productId,
        long colorId,
        long sizeId,
        long createdBy,
        Money money,
        int stockQuantity,
        string sku)
        {
            ProductId = productId;
            ColorId = colorId;
            SizeId = sizeId;
            CreatedBy = createdBy;
            SetPrice(money);
            SetStock(stockQuantity);
            SetSku(sku);

            IsActive = true;
        }

        public long ProductId { get; private set; }
        public long ColorId { get; private set; }
        public long CreatedBy { get; private set; }
        public long SizeId { get; private set; }
        public Money Money { get; private set; }

        public int StockQuantity { get; private set; }

        public string SKU { get; private set; } = string.Empty;

        public bool IsActive { get; private set; }

        public UserProfile UserProfile { get; private set; } = null!;
        public Product Product { get; private set; } = null!;

        private readonly List<ProductImage> _images = [];

        public Color Color { get; private set; } = null!;

        public Size Size { get; private set; } = null!;


        public IReadOnlyCollection<ProductImage> Images
            => _images.AsReadOnly();


        public void SetPrice(Money money)
        {
            Money = money;
        }

        public void SetStock(int stockQuantity)
        {
            if (stockQuantity < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(stockQuantity),
                    "Stock cannot be negative.");

            StockQuantity = stockQuantity;
        }

        public void IncreaseStock(int amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            StockQuantity += amount;
        }

        public void DecreaseStock(int amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            if (StockQuantity - amount < 0)
                throw new InvalidOperationException("Insufficient stock.");

            StockQuantity -= amount;
        }
        public void SetSku(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku))
                throw new ArgumentException(
                    "SKU cannot be empty.",
                    nameof(sku));

            SKU = sku.Trim().ToUpperInvariant();
        }

        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;


        public void UpdateVariant(ProductVariant variant)
        {
            ProductId = variant.ProductId;
            ColorId = variant.ColorId;
            SizeId = variant.SizeId;
            CreatedBy = variant.CreatedBy;
            SetPrice(variant.Money);
            SetStock(variant.StockQuantity);
            SetSku(variant.SKU);
            IsActive = variant.IsActive;
            MarAsUpdated();
        }


    }
}
