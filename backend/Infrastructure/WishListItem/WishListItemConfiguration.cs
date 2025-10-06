using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.WishListItem
{
    public class WishListItemConfiguration : IEntityTypeConfiguration<Domain.Entities.WishListItem>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.WishListItem> builder)
        {
            builder.ToTable("wishListItems");

            builder.HasKey(wi => wi.WishListItemId);
            builder.Property(wi => wi.WishListItemId).HasColumnName("wishListItemsId");

            builder.Property(wi => wi.EmployeeId).HasColumnName("employeeId");
            builder.Property(wi => wi.ProductVariantId).HasColumnName("productVariantId");
            builder.Property(wi => wi.DateAdded).HasColumnName("dateAdded");

            builder.HasOne(wi => wi.Employee)
                .WithMany(e => e.WishListItems)
                .HasForeignKey(wi => wi.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(wi => wi.ProductVariant)
                .WithMany()
                .HasForeignKey(wi => wi.ProductVariantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(wi => new { wi.EmployeeId, wi.ProductVariantId }).IsUnique();
        }
    }
}
