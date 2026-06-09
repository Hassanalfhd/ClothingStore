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
    public sealed class OrderItemConfiguration
     : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProductName)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.UnitPrice)
                .HasPrecision(18, 2);

            builder.Property(x => x.Quantity)
                .IsRequired();
        }
    }
}
