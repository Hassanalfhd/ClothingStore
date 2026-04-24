using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Domain.DomainEvents;
using MediatR;

namespace ClothingStore.Application.Common.Events
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private IMediator _mediator;

        public DomainEventDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }


        public async Task DispatchAsync(IEnumerable<object> domainEvents)
        {
            foreach (var domainEvent in domainEvents)
            {
                var notificationType = typeof(DomainEventNotification<>)
                    .MakeGenericType(domainEvent.GetType());

                var notification = Activator.CreateInstance(notificationType, domainEvent);

                await _mediator.Publish(notification);
            }
        }

    }
}
