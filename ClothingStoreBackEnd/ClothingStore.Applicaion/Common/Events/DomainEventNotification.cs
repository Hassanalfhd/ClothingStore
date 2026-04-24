using MediatR;

namespace ClothingStore.Application.Common.Events;
public record DomainEventNotification<IDomainEvent>(IDomainEvent DomainEvent)
    : INotification;
