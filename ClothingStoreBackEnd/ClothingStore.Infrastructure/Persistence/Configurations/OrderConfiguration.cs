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
    public sealed class OrderConfiguration: IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.PublicId)
                .IsRequired();

            builder.HasIndex(x => x.PublicId)
                .IsUnique();

            builder.Property(x => x.OrderNumber)
        .HasMaxLength(50);

            builder.HasIndex(x => x.OrderNumber)
                .IsUnique();

            builder.Property(x => x.TotalAmount)
          .HasPrecision(18, 2);

            builder.Property(x => x.ShippingCost)
                .HasPrecision(18, 2);

            builder.Property(x => x.DiscountAmount)
                .HasPrecision(18, 2);

            builder.Property(x => x.RecipientName)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.PhoneNumber)
          .HasMaxLength(30)
          .IsRequired();

            builder.Property(x => x.City)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.AddressLine)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(x => x.CancellationReason)
         .HasMaxLength(500);

            builder.Property(x => x.Status)
                .HasConversion<int>();


            builder.Property(x => x.PaymentStatus)
                .HasConversion<int>();

            builder.HasMany(x => x.Items)
          .WithOne()
          .HasForeignKey(x => x.OrderId)
          .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
