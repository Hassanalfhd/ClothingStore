using ClothingStore.Application.Common.Events;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Application.Interfaces.Services;
using ClothingStore.Domain.DomainEvents;
using ClothingStore.Domain.ValueObjects;
using MediatR;

namespace ClothingStore.Application.Features.User.UserProfile
{
    public class UserRegisteredHandler : INotificationHandler<DomainEventNotification<UserRegisteredEvent>>
    {
        private readonly IAppLogger<UserRegisteredHandler> _logger;

        private readonly IUserRepo _userRepo;
        public UserRegisteredHandler(IAppLogger<UserRegisteredHandler> logger, IUserRepo userRepo)
        {
            _logger = logger;
            _userRepo = userRepo;
        }


        public Task Handle(DomainEventNotification<UserRegisteredEvent> notification, CancellationToken cancellationToken = default)
        {
            var e = notification.DomainEvent;


            var contactInfo = new ContactInfo(e.Email, null, null);

            var userProfile = new ClothingStore.Domain.Entities.UserProfile(
                e.UserId,
                contactInfo,
                e.FirstName,
                e.LastName,
                birthDate: null,
                profileImage: null

            );

            _userRepo.Add(userProfile);


            _logger.LogInformation($"User Registered: {e.UserId}");

            return Task.CompletedTask;

        }

    }
}
