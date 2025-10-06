using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Attributes
{
    public class AttributesConfiguration : IEntityTypeConfiguration<Domain.Entities.Attributes>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Attributes> builder)
        {
            builder.ToTable("attributes");

            builder.HasKey(a => a.AttributesId);
            builder.Property(a => a.AttributesId).HasColumnName("attributesId");

            builder.Property(a => a.Name).HasColumnName("name");

            builder.HasIndex(a => a.Name).IsUnique(); 
        }
    }
}
