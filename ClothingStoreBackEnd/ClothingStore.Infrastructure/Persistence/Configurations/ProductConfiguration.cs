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
    public class ProductConfiguration: IEntityTypeConfiguration<Product>
    {

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.Id);

            // INDEXES (Performance)
            builder.HasIndex(x => x.PublicId).IsUnique();
            builder.HasIndex(x => x.Name);
            builder.HasIndex(x => x.CategoryId);
            builder.HasIndex(x => x.IsActive);


            builder.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(x => x.Description)
                .HasMaxLength(2000);

            builder.OwnsOne(x => x.BasePrice, price =>
            {
                price.Property(p => p.Amount)
                    .HasColumnName("BasePrice")
                    .HasPrecision(18, 2);

                price.Property(p => p.Currency)
                    .HasColumnName("Currency")
                    .HasMaxLength(3);
            });



            builder.Property(x => x.IsActive)
               .HasDefaultValue(true);


            builder.HasOne(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Variants)
                 .WithOne(x => x.Product)
                 .HasForeignKey(x => x.ProductId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Images)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);




        }


    }
}
