using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Domain.Entities
{

    public sealed class Size : EntityBased
    {
        private readonly List<ProductVariant> _productVariants = [];

        private Size()
        {
            // Required by EF Core
        }

        public Size(string name, int displayOrder)
        {
            SetName(name);
            SetDisplayOrder(displayOrder);

            IsActive = true;
        }

        public string Name { get; private set; } = string.Empty;

        public int DisplayOrder { get; private set; }

        public bool IsActive { get; private set; }

        public IReadOnlyCollection<ProductVariant> ProductVariants
            => _productVariants.AsReadOnly();

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(
                    "Size name cannot be empty.",
                    nameof(name));

            Name = name.Trim().ToUpperInvariant();
        }


        public void SetDisplayOrder(int displayOrder)
        {
            if (displayOrder < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(displayOrder),
                    "Display order cannot be negative.");

            DisplayOrder = displayOrder;
        }

        public void Activate() => IsActive = true;

        public void Deactivate() => IsActive = false;

        public override string ToString() => Name;
    }

}
