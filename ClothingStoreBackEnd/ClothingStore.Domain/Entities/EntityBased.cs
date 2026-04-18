using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Domain.Entities
{
    public abstract class EntityBased
    {
        public Guid PublicId { get; private set; }

        public long Id { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        protected EntityBased()
        {
            PublicId = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }
        public void MarAsUpdated() => UpdatedAt = DateTime.Now;



    }
}
