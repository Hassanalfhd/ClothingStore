using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Infrastructure.Persistence.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ImageUrl)
                .IsRequired();

            builder.Property(x => x.IsPrimary)
                .HasDefaultValue(false);

            builder.Property(x => x.DisplayOrder)
                .HasDefaultValue(0);

            builder.Property(x => x.IsProcessed)
                .HasDefaultValue(false);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.Images)
                .HasForeignKey(x => x.ProductId);

            builder.HasOne(x => x.ProductVariant)
                .WithMany()
                .HasForeignKey(x => x.ProductVariantId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasIndex(x => x.ProductId);
        }
    }
}
