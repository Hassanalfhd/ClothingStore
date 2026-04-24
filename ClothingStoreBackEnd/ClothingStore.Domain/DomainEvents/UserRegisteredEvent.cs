

namespace ClothingStore.Domain.DomainEvents;
    public  record UserRegisteredEvent(long UserId, string Email, string FirstName, string LastName) : IDomainEvent;
    

