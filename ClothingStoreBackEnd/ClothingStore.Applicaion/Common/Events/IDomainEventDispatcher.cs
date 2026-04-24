using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Domain.DomainEvents;

namespace ClothingStore.Application.Common.Events
{
    public interface IDomainEventDispatcher
    {
        Task DispatchAsync(IEnumerable<Object> domainEvents);
    }
}
