using ClothingStore.Domain.Entities;
using Microsoft.AspNetCore.Identity;


namespace ClothingStore.Identity.Models
{
    public class ApplicationUser : IdentityUser<long>
    {
        public Guid PublicId { get; private set; } 
        public string FirstName { get; private set; }
        public string LastName{ get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsActive { get; private set; }

        public virtual UserProfile UserProfile { get; private set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }

        public ApplicationUser() { }

        public ApplicationUser(string email, string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException(nameof(email));
         
            PublicId = Guid.NewGuid();
            Email = email;
            UserName = email;
            FirstName = firstName;
            LastName = lastName;
            CreatedAt = DateTime.UtcNow;
            IsActive = true;

        }

        public void UpdateName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Names cannot be empty");

            FirstName = firstName;
            LastName = lastName;


        }


        public void Deactivate() => IsActive = false;
        public void Activate() => IsActive = true;

        public void SetProfile(UserProfile userProfile)
        {
            UserProfile = userProfile;
        }

        public void SetRefreshToken(RefreshToken refreshToken )
        {
            RefreshTokens.Add(refreshToken);
        }


    }
}
