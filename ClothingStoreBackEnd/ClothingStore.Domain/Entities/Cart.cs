using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Domain.Entities
{
    public class Cart:EntityBased
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


        public void Clear()
        {
            Items.Clear();
            MarkAsUpdated();
        }

        public void MarkAsCheckedOut()
        {
            IsCheckedOut = true;
            MarkAsUpdated();
        }


    }
}
