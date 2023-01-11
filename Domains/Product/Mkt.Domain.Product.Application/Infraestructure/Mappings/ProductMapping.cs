using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mkt.Domain.Product.Application.Infraestructure.Entities;

namespace Mkt.Domain.Product.Application.Infraestructure.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.HasKey(p => p.ProductId);

            builder.Property(p => p.ProductId)
                .HasColumnName("productId")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.DisplayName)
                .HasColumnName("displayName")
                .IsRequired();

            builder.Property(p => p.Quantity)
                .HasColumnName("quantity")
                .HasDefaultValue(0);

            builder.Property(p => p.UnitPrice)
                .HasColumnName("unityPrice")
                .HasDefaultValue(0);

            builder.ToTable("products");
        }
    }
}
