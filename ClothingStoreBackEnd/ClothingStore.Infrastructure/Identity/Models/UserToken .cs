using Microsoft.AspNetCore.Identity;

namespace ClothingStore.Identity.Models
{
    
    public class UserToken : IdentityUserToken<long>
    {
        public bool IsRevoked { get; set; }
        public bool IsUsed { get; set; }
        public DateTime ExpiryDate { get; set; }

        public bool IsExpired => DateTime.UtcNow >= ExpiryDate;
        public bool IsActive => !IsUsed && !IsRevoked && !IsExpired;
    }
}
