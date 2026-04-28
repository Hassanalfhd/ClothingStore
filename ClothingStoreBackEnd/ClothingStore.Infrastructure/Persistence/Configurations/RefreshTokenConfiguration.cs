using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Domain.Entities;
using ClothingStore.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothingStore.Infrastructure.Persistence.Configurations
{
    public class RefreshTokenConfiguration: IEntityTypeConfiguration<RefreshToken>
    {

        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {


            builder.ToTable("RefreshTokens");


            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd();


            builder.HasIndex(r => r.PublicId)
                .IsUnique();

            builder.HasIndex(r=>r.Token).IsUnique().IncludeProperties(x=>new
            {
                x.Id,
                x.PublicId,
                x.ApplicationUserId,
                x.IsUsed,
                x.IsRevoked,
                x.UpdatedAt,
                x.ExpiryDate,
                x.CreatedAt
            });


            builder.Property(r => r.ApplicationUserId).IsRequired();

            builder.Property(r=>r.IsUsed).HasDefaultValue(false);
            builder.Property(r=>r.IsRevoked).HasDefaultValue(false);

            builder.Property(r => r.CreatedAt).HasDefaultValue(DateTime.UtcNow);
            
        }

    }
}
