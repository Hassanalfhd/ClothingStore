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

        public long VariantId { get; private set; }
        public Guid VariantPublicId { get; private set; }
        public string ProductName { get; private set; }
        public Money UnitPrice { get; private set; }
        public int Quantity { get; private set; }

        private CartItem() { } // EF Core

        public CartItem(
            long cartId,
            long productId,
            long variantId,
            Guid variantPublicId,
            string productName,
            Money unitPrice,
            int quantity)
        {
            CartId = cartId;
            ProductId = productId;
            VariantId = variantId;
            VariantPublicId = variantPublicId;
            ProductName = productName;
            SetUnitPrice(unitPrice);
            UpdateQuantity(quantity);
        }

        public void SetUnitPrice(Money unitPrice)
        {
            UnitPrice = unitPrice;

        }

        public void UpdateQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new InvalidOperationException("Quantity cannot be less than 1.");

            Quantity = quantity;
        }
      
        public void IncreaseQuantity()
        {
            Quantity++;
            MarkAsUpdated();
        }

        public void DecreaseQuantity()
        {
            if (Quantity <= 1)
                throw new InvalidOperationException("Quantity cannot be less than 1.");

            Quantity--;
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
