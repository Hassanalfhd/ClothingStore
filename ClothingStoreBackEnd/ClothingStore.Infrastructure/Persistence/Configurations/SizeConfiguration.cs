using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothingStore.Infrastructure.Persistence.Configurations
{
    public class SizeConfiguration: IEntityTypeConfiguration<Size>
    {
        public void Configure(EntityTypeBuilder<Size> builder)
        {
            builder.ToTable("Sizes");

            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.PublicId).IsUnique();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.DisplayOrder)
                .IsRequired();

            builder.HasMany(x => x.ProductVariants)
                .WithOne(x => x.Size)
                .HasForeignKey(x => x.SizeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.Name).IsUnique();
            builder.HasIndex(x => x.DisplayOrder);
        }
    }
}
