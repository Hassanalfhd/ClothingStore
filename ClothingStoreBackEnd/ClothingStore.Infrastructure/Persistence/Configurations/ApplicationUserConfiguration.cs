
using ClothingStore.Domain.Entities;
using ClothingStore.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothingStore.Infrastructure.Persistence.Configurations
{
    public  class ApplicationUserConfiguration: IEntityTypeConfiguration<ApplicationUser>
    {

        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {

            builder.ToTable("Users");


            builder.HasIndex(u => u.PublicId)
                .IsUnique();

            builder.Property(u => u.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(u => u.LastName).HasMaxLength(50).IsRequired();


            builder.HasOne(e => e.UserProfile)
                .WithOne()
                .HasForeignKey<UserProfile>(ur => ur.ApplicationUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(u => u.RefreshTokens)
                .WithOne()
                .HasForeignKey(rt => rt.ApplicationUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        
        }

    }
}
