using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ProductVariants
{
    public class ProductVariantsConfiguration : IEntityTypeConfiguration<Domain.Entities.ProductVariants>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.ProductVariants> builder)
        {
            builder.ToTable("productVariants");

            builder.HasKey(pv => pv.ProductVariantsId);
            builder.Property(pv => pv.ProductVariantsId).HasColumnName("productVariantId");

            builder.Property(pv => pv.Price).HasColumnName("price");

            builder.HasOne(pv => pv.Product)
                .WithMany(p => p.ProductVariants)
                .HasForeignKey(pv => pv.ProductId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
