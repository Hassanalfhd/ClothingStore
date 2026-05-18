using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Domain.Entities
{
    public class ProductSpecification
    {
        public long Id { get; private set; }
        public long ProductId { get; private set; }

        public string Key { get; private set; } = string.Empty;
        public string Value { get; private set; } = string.Empty;

        public Product Product { get; private set; } = null!;
    }
}
