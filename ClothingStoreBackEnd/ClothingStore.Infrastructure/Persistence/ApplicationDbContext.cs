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

        public DbSet<Category> Categories { get; set; }
        public DbSet<Color> Colors { get; set; }

        public DbSet<Size> Sizes { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariant> ProductsVariant { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }



    }

}
