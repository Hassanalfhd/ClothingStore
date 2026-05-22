using ClothingStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothingStore.Infrastructure.Persistence.Configurations
{
    public class BrandConfiguration: IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {

            builder.ToTable("Brands");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.PublicId);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.Slug)
                .IsRequired()
                .HasMaxLength(20);


            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");



            builder.HasIndex(x => x.Name)
                .IsUnique();


        }
    }
}
