using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Domain.Entities
{
    public class Order: EntityBased
    {

        public long CustomerId { get; private set; }// UserId for Customer 
    
    }

}
