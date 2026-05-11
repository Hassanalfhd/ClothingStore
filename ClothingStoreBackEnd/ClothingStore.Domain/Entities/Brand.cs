using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Domain.Entities
{
    public class Brand: EntityBased
    {

        private readonly List<Product> _products = [];


        public Brand() { }


        public Brand(string name, string slug, string? description, string? logoUrl)
        {
            SetName(name);
            SetDescription(description);
            SetSlug(slug);
            LogoUrl = logoUrl;
        }



        public string Name { get; private set; }
        public string Slug { get; set; } = null!;
        public string Description { get; private set; }
        public string? LogoUrl { get; private set; } = null;

        public IReadOnlyCollection<Product> Products => _products.AsReadOnly();


        

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("Brand name cannot be empty.", nameof(name));

            Name = name.Trim();
        }

        public void SetDescription(string? description)
        {
            Description = string.IsNullOrWhiteSpace(description)
                ? null
                : description.Trim();
        }


        public void SetSlug(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug)) throw new ArgumentNullException("Brand slug cannot be empty.", nameof(slug));

            Slug = slug.Trim();
        }


    }
}
