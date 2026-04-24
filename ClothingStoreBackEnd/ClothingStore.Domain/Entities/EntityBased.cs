using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Domain.Common.Interfaces;
using ClothingStore.Domain.DomainEvents;

namespace ClothingStore.Domain.Entities
{
    public abstract class EntityBased : IHasDomainEvents
    {
        public Guid PublicId { get; private set; }

        public long Id { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? UpdatedAt { get; private set; }


        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyCollection<IDomainEvent> DomainEvents { get
            {
                return _domainEvents;
            } }

        protected EntityBased()
        {
            PublicId = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }


        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }


        public void MarAsUpdated() => UpdatedAt = DateTime.Now;



    }
}
