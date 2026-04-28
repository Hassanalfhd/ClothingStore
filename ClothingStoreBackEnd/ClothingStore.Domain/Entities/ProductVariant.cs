using ClothingStore.Domain.ValueObjects;

namespace ClothingStore.Domain.Entities
{
    public class ProductVariant : EntityBased
    {

        private ProductVariant() { }


        public ProductVariant(
        long productId,
        long colorId,
        long sizeId,
        Money price,
        int stockQuantity,
        string sku)
        {
            ProductId = productId;
            ColorId = colorId;
            SizeId = sizeId;

            SetPrice(price);
            SetStock(stockQuantity);
            SetSku(sku);

            IsActive = true;
        }

        public long ProductId { get; private set; }
        public long ColorId { get; private set; }
        public long SizeId { get; private set; }
        public Money Price { get; private set; }

        public int StockQuantity { get; private set; }

        public string SKU { get; private set; } = string.Empty;

        public bool IsActive { get; private set; }

        public Product Product { get; private set; } = null!;

        public Color Color { get; private set; } = null!;

        public Size Size { get; private set; } = null!;

        public void SetPrice(Money price)
        {
            Price = price;
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

    }
}
