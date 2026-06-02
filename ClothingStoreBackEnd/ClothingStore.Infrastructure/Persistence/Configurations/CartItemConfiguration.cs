using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ClothingStore.Infrastructure.Persistence.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItems");

            builder.HasKey(x => x.Id);


            builder.Property(x => x.CartId)
                .IsRequired();

            builder.Property(x => x.PublicId).IsRequired();

            builder.HasIndex(x => x.PublicId).IsUnique();

            builder.Property(x => x.VariantId)
                .IsRequired();

            builder.Property(x => x.ProductName)
                .IsRequired()
                .HasMaxLength(200);

            builder.OwnsOne(x => x.UnitPrice, price =>
            {
                price.Property(p => p.Amount)
                    .HasColumnName("UnitPrice")
                    .HasPrecision(18, 2);

                price.Property(p => p.Currency)
                    .HasColumnName("Currency")
                    .HasMaxLength(10);
            });


            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");


            builder.HasOne<Product>()
                    .WithMany()
                      .HasForeignKey(x => x.ProductId)
                      .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne<ProductVariant>()
                    .WithMany()
                      .HasForeignKey(x => x.VariantId)
                      .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
