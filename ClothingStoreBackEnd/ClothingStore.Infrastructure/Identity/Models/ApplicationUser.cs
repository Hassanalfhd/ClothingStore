using System;
using ClothingStore.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace ClothingStore.Identity.Models
{
    public class ApplicationUser : IdentityUser<long>
    {
        public string FullName { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsActive { get; private set; }


        public ApplicationUser() { }

        public ApplicationUser(string email, string fullName)
        {
            Email = email;
            UserName = email;
            FullName = fullName;
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }


        public void Deactivate() => IsActive = false;

        public UserProfile? Profile { get; set; }
    }
}
