using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Domain.Entities
{
    public class OrderItem: EntityBased
    {
        private OrderItem() { }
        public long OrderId { get; private set; }

        public long ProductId { get; private set; }

        public long VariantId { get; private set; }

        public string ProductName { get; private set; } = string.Empty;

        public decimal UnitPrice { get; private set; }

        public int Quantity { get; private set; }

        public decimal SubTotal => UnitPrice * Quantity;



        public static OrderItem Create(
         long productId,
         long variantId,
         string productName,
         decimal unitPrice,
         int quantity)
        {
            if (productId <= 0)
                throw new ArgumentException(nameof(productId));

            if (variantId <= 0)
                throw new ArgumentException(nameof(variantId));

            if (string.IsNullOrWhiteSpace(productName))
                throw new ArgumentException(nameof(productName));

            if (unitPrice <= 0)
                throw new ArgumentException(nameof(unitPrice));

            if (quantity <= 0)
                throw new ArgumentException(nameof(quantity));

            return new OrderItem
            {
                ProductId = productId,
                VariantId = variantId,
                ProductName = productName,
                UnitPrice = unitPrice,
                Quantity = quantity
            };
        }


    }
}
