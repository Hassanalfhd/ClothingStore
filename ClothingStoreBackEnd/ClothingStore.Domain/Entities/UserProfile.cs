using ClothingStore.Domain.DomainEvents;
using ClothingStore.Domain.ValueObjects;

namespace ClothingStore.Domain.Entities
{
    public class UserProfile : EntityBased
    {

        public long ApplicationUserId { get; private set; }

        // value object
        public ContactInfo ContactInfo { get; private set; } = new ContactInfo(null, null, null);


        // additional data 
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }
        public string FullName => $"{FirstName} {LastName}";

        public DateTime? BirthDate { get; private set; }

        public string? ProfileImage { get; private set; }

        public bool IsAdult => BirthDate.HasValue && BirthDate.Value.AddYears(18) <= DateTime.UtcNow;


        private UserProfile() { } // for EF 

        public UserProfile(long applicationUserId, ContactInfo contactInfo, string? firstName, string? lastName, DateTime? birthDate, string? profileImage)
        {
            ApplicationUserId = applicationUserId;
            ContactInfo = contactInfo;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            ProfileImage = profileImage;

        }



        public void UpdatePersonalData(string? firstName, string? lastName, DateTime? birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
        }
        public void UpdateContactInfo(ContactInfo newInfo)
        {
            ContactInfo = newInfo ?? throw new ArgumentNullException(nameof(newInfo));
        }



    }
}
