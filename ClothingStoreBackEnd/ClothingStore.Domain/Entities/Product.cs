 

using ClothingStore.Domain.Enums;
using ClothingStore.Domain.ValueObjects;

namespace ClothingStore.Domain.Entities
{
    public class Product : EntityBased
    {
        private readonly List<ProductVariant> _variants = [];

        private readonly List<ProductImage> _images = [];
        private readonly List<ProductSpecification> _specifications = new();

        private Product() { }

        public Product(string name, string description, Money basePrice, bool isActive, long createdBy, long categoryId, long? brandId)
        {
            CreatedBy = createdBy;
            CategoryId = categoryId;
            IsActive = isActive;
            BrandId = brandId;
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
        public long? BrandId { get; private set; }

        public Category Category { get; private set; } = null!;
        public Brand? Brand { get; private set; } = null!;
        public UserProfile UserProfile { get; private set; } = null!;

        public ProductStatus Status { get; private set; }

        public IReadOnlyCollection<ProductVariant> Variants
            => _variants.AsReadOnly();

        public IReadOnlyCollection<ProductImage> Images
            => _images.AsReadOnly();

        public IReadOnlyCollection<ProductSpecification> Specifications 
            => _specifications.AsReadOnly();

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



        public void UpdateProduct(Product product)
        {
            SetName(product.Name);
            SetDescription(product.Description);
            SetBasePrice(product.BasePrice);

            CategoryId = product.CategoryId;
            CreatedBy = product.CreatedBy;
            BrandId = product.BrandId;
            base.MarkAsUpdated();
        }

        public void AddSepecifiaction(string key, string value)
        {
            _specifications.Add(new ProductSpecification(Id, key, value));
        }

        public void RemoveSpecification(string key)
        {
            var specification = _specifications
                .FirstOrDefault(x =>
                    x.Key.ToLower() == key.ToLower());

            if (specification is not null)
            {
                _specifications.Remove(specification);
            }
        }

        public void ClearSpecifications()
        {
            _specifications.Clear();
        }

        public void Activate() => IsActive = true;

        public void Deactivate() => IsActive = false;

        public void ToggleActivate() => IsActive = !IsActive;

        public override string ToString() => Name;

    }
}
