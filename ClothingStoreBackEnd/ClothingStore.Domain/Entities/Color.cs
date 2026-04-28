using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Domain.Entities
{
    public class Color : EntityBased
    {
        private readonly List<ProductVariant> _productVariants = [];
    
        private Color() { }

        public string Name { get; private set; } = string.Empty;

        public string HexCode { get; private set; } = string.Empty;

        public IReadOnlyCollection<ProductVariant> ProductVariants
            => _productVariants.AsReadOnly();

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(
                    "Color name cannot be empty.",
                    nameof(name));

            Name = name.Trim();
        }

        public void SetHexCode(string hexCode)
        {
            if (string.IsNullOrWhiteSpace(hexCode))
                throw new ArgumentException(
                    "Hex code cannot be empty.",
                    nameof(hexCode));

            hexCode = hexCode.Trim().ToUpperInvariant();

            if (!IsValidHexCode(hexCode))
                throw new ArgumentException(
                    "Invalid hex color code format.",
                    nameof(hexCode));

            HexCode = hexCode;
        }

        private static bool IsValidHexCode(string hexCode)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(
                hexCode,
                "^#(?:[0-9A-F]{3}|[0-9A-F]{6})$");
        }
        
        public override string ToString() => Name;

    }

}
