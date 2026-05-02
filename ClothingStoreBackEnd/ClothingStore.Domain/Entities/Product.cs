

using ClothingStore.Domain.ValueObjects;

namespace ClothingStore.Domain.Entities
{
    public class Product : EntityBased
    {
        private readonly List<ProductVariant> _variants = [];
        private readonly List<ProductImage> _images = [];

        private Product() { }


        public Product(string name, string description, Money basePrice, bool isActive, long createdBy, long categoryId)
        {
            CreatedBy = createdBy;
            CategoryId = categoryId;
            IsActive = isActive;

            SetName(name);
            SetDescription(description);
            SetBasePrice(basePrice);

        }


        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;

        public Money BasePrice { get; private set; }
        public bool IsActive { get; private set; }

        public long CreatedBy { get; private set; }
        public long CategoryId { get; private set; }

        public Category Category { get; private set; } = null!;
        public UserProfile UserProfile { get; private set; } = null!;


        public IReadOnlyCollection<ProductVariant> Variants
            => _variants.AsReadOnly();

        public IReadOnlyCollection<ProductImage> Images
            => _images.AsReadOnly();

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(
                    "Product name cannot be empty.",
                    nameof(name));

            Name = name.Trim();
        }


        public void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException(
                    "Product description cannot be empty.",
                    nameof(description));

            Description = description.Trim();
        }

        public void SetBasePrice(Money price)
        {
            BasePrice = price;
        }


        public void Activate() => IsActive = true;

        public void Deactivate() => IsActive = false;

        public override string ToString() => Name;


    }
}
