using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Domain.ValueObjects;

namespace ClothingStore.Domain.Entities
{
    public class CartItem: EntityBased
    {

        public long CartId { get; private set; }

        public long ProductId { get; private set; }

        public long? VariantId { get; private set; }

        public string ProductName { get; private set; }

        public string? VariantName { get; private set; }

        public Money UnitPrice { get; private set; }
        public int Quantity { get; private set; }


        private CartItem() { } // EF Core

        public CartItem(
            long cartId,
            long productId,
            long? variantId,
            string productName,
            string? variantName,
            Money unitPrice,
            int quantity)
        {
            CartId = cartId;
            ProductId = productId;
            VariantId = variantId;
            ProductName = productName;
            VariantName = variantName;
            SetUnitPrice(unitPrice);
            SetQuantity(quantity);
        }

        public void SetUnitPrice(Money unitPrice)
        {
            UnitPrice = unitPrice;

        }

        public void SetQuantity(int quantity)
        {
            Quantity = quantity;
            MarkAsUpdated();
        }
        public void IncreaseQuantity(int quantity)
        {
            if (quantity <= 0)
                return;

            Quantity += quantity;
            MarkAsUpdated();
        }
    }
}
