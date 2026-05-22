using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ClothingStore.Domain.Enums;

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

            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.Processed)
                .HasDefaultValue(Processed.Pending);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.Images)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.ProductVariant)
                .WithMany(x=>x.Images)
                .HasForeignKey(x => x.ProductVariantId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasIndex(x => x.ProductId);

            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_ProductImage_Product_Or_Variant",
                        @"(ProductId IS NOT NULL AND ProductVariantId IS NULL)
                    OR
                    (ProductId IS NULL AND ProductVariantId IS NOT NULL)"
            );
            });
        }
    }
}
