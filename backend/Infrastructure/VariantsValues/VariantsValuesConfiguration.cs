using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.VariantsValues
{
    public class VariantsValuesConfiguration : IEntityTypeConfiguration<Domain.Entities.VariantValues>
    {
        public void Configure(EntityTypeBuilder<VariantValues> builder)
        {
            builder.ToTable("variantValues");

            builder.HasKey(vv => vv.VariantValuesId);
            builder.Property(vv => vv.VariantValuesId).HasColumnName("variantValueId");

            builder.HasOne(vv => vv.ProductVariant)
                .WithMany(pv => pv.VariantValues)
                .HasForeignKey(vv => vv.ProductVariantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(vv => vv.AttributeValue)
                .WithMany(av => av.VariantValues)
                .HasForeignKey(vv => vv.AttributeValueId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
