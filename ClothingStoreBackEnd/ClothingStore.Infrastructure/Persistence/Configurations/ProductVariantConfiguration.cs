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
    public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
    {
        public void Configure(EntityTypeBuilder<ProductVariant> builder)
        {
            builder.ToTable("ProductVariants");

            builder.HasKey(x => x.Id);

            builder.OwnsOne(x => x.Price, price =>
            {
                price.Property(p => p.Amount)
                    .HasColumnName("Price")
                    .HasPrecision(18, 2);

                price.Property(p => p.Currency)
                    .HasColumnName("Currency")
                    .HasMaxLength(3);
            });

            builder.Property(x => x.StockQuantity)
                .IsRequired();


            builder.Property(x => x.SKU)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasOne(x => x.Product)
                .WithMany(x => x.Variants)
                .HasForeignKey(x => x.ProductId);

            builder.HasOne(x => x.Color)
                .WithMany(x => x.ProductVariants)
                .HasForeignKey(x => x.ColorId);

            builder.HasOne(x => x.Size)
                .WithMany(x => x.ProductVariants)
                .HasForeignKey(x => x.SizeId);

            builder.HasOne(u => u.UserProfile)
                .WithMany(v => v.ProductVariants)
                .HasForeignKey(x => x.CreatedBy);


            // منع التكرار (Color + Size لكل Product)
            builder.HasIndex(x => new { x.ProductId, x.ColorId, x.SizeId })
                .IsUnique();

            
            builder.HasIndex(x => x.SKU)
                .IsUnique();

            // ✅ CHECK CONSTRAINT
            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_ProductVariants_StockQuantity_NonNegative",
                    "[StockQuantity] >= 0");
                t.HasCheckConstraint(
                   "CK_ProductVariant_Price_Positive",
                   "[Price] >= 0");

            });


        }
    }
}
