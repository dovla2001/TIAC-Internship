using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.AttributesValues
{
    public class AttributesValuesConfiguration : IEntityTypeConfiguration<Domain.Entities.AttributesValues>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.AttributesValues> builder)
        {
            builder.ToTable("attributeValues");

            builder.HasKey(av => av.AttributesValuesId);
            builder.Property(av => av.AttributesValuesId).HasColumnName("attributesValuesId");

            builder.Property(av => av.Value).HasColumnName("value");

            builder.HasOne(av => av.Attribute)
                .WithMany(a => a.AttributeValues)
                .HasForeignKey(av => av.AttributeId);
        }
    }
}
