using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Domain.Entities
{
    public class Category : EntityBased
    {

        private readonly List<Product> _products = [];
        private Category()
        {
            // Required EF Core }
        }

        public Category(string name, string description)
        {
            SetName(name);
            SetDescription(description);
        }

        public string Name { get; private set; } = string.Empty;
        public string? Description { get; private set; }

        public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("Category name cannot be empty.", nameof(name));

            Name=  name.Trim();
        }

        public void SetDescription(string? description)
        {
            Description = string.IsNullOrWhiteSpace(description)
                ? null
                : description.Trim();
        }

        public override string ToString() => Name;

    }
}
