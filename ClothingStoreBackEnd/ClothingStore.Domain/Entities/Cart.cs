using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Domain.Common;
using ClothingStore.Domain.ValueObjects;

namespace ClothingStore.Domain.Entities
{
    public class Cart: EntityBased
    {

        public long? UserId { get; private set; } // null = Guest Cart

        public List<CartItem> Items { get; private set; } = new();

        public bool IsCheckedOut { get; private set; }

        private Cart() { } // EF Core

        public Cart(long? userId)
        {
            UserId = userId;
            IsCheckedOut = false;
        }

        public void AddItem(
           long productId,
           long variantId,
           Guid variantPublicId,
           string productName,
            Money unitPrice,
           int quantity)
        {
            if (quantity <= 0)
                throw new InvalidOperationException("Quantity must be greater than 0");

            var existingItem = Items.FirstOrDefault(x =>
                x.ProductId == productId &&
                x.VariantId == variantId);

            if (existingItem is not null)
            {
                existingItem.IncreaseQuantity(quantity);
                MarkAsUpdated();
                return;
            }

            var item = new CartItem(
                Id,  // CartId             
                productId,
                variantId,
                variantPublicId,
                productName,
                unitPrice,
                quantity);

            Items.Add(item);
            MarkAsUpdated();
        }

        public void Clear()
        {
            Items.Clear();
            MarkAsUpdated();
        }

      
        public Result RemoveItem(long variantId)
        {
            var item = Items.FirstOrDefault(x => x.VariantId == variantId);
            if (item is null)
                return Result.Failure("Item not found");

            Items.Remove(item);

            MarkAsUpdated();

            return Result.Success();
        }

        public void MarkAsCheckedOut()
        {
            IsCheckedOut = true;
            MarkAsUpdated();
        }


    }
}
