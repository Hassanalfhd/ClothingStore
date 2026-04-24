using System.Reflection;
using ClothingStore.Domain.Entities;
using ClothingStore.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Infrastructure.Persistence
{
    public class ApplicationDbContext: IdentityDbContext<
        ApplicationUser,
        Role,
        long
        >
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        :base(options)
        {

        }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<RefreshToken>RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }



    }

}
