using ClothingStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ClothingStore.Persistence.Configurations
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd();
            
            builder.HasIndex(u => u.PublicId).IsUnique();


            builder.Property(u => u.ApplicationUserId)
                .IsRequired();

            
            builder.Property(u => u.FirstName).HasMaxLength(50).IsRequired(false);
            builder.Property(u => u.LastName).HasMaxLength(50).IsRequired(false);

            builder.Property(u => u.ProfileImage)
               .HasMaxLength(500)
               .IsRequired(false);


            builder.OwnsOne(u => u.ContactInfo, contact =>
            {
                contact.Property(c => c.Email).HasMaxLength(50).IsRequired();
                contact.HasIndex(c => c.Email).IsUnique();
                contact.Property(c => c.PhoneNumber).HasMaxLength(20).IsRequired(false);
                contact.Property(c => c.Address).HasMaxLength(50).IsRequired(false);
            });


            builder.Property(u => u.CreatedAt)
            .HasDefaultValue(DateTime.UtcNow);

            builder.Property(u => u.UpdatedAt)
                   .IsRequired(false);

            builder.Ignore(u => u.IsAdult);
            builder.Ignore(u => u.FullName);
        
        }
    }
}
