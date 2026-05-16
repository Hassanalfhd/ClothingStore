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
    public class ColorConfiguration: IEntityTypeConfiguration<Color>
    {

        public void Configure(EntityTypeBuilder<Color> builder)
        {

            builder.ToTable("Colors");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.PublicId);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.HexCode)
                .HasMaxLength(7)
                .IsRequired();

            

            builder.HasIndex(x => x.Name)
                .IsUnique();


        }
    }
}
