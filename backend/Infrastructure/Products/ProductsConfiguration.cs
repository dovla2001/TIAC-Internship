using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Products
{
    public class ProductsConfiguration : IEntityTypeConfiguration<Domain.Entities.Products>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Products> builder)
        {
            builder.ToTable("products");

            builder.HasKey(p => p.ProductId);
            builder.Property(p => p.ProductId).HasColumnName("productId");

            builder.Property(p => p.Name).HasColumnName("name");

            builder.Property(p => p.Decsription).HasColumnName("description");

            builder.Property(p => p.BasePrice).HasColumnName("basePrice");

            builder.Property(p => p.ImageUrl).HasColumnName("imageUrl");

        }
    }
}
