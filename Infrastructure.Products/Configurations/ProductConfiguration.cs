
using Infrastructure.Products.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Products.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder
                .Property(p => p.CreatedDate)
                .IsRequired();

            builder
                .Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(p => p.Description)
                .HasMaxLength(150)
                .IsRequired(false);
        }
    }
}
