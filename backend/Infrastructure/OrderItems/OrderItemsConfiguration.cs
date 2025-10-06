using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.OrderItems
{
    public class OrderItemsConfiguration : IEntityTypeConfiguration<Domain.Entities.OrderItems>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.OrderItems> builder)
        {
            builder.ToTable("orderItems");

            builder.HasKey(oi => oi.OrderItemId);
            
            builder.Property(oi => oi.OrderItemId).HasColumnName("orderItemId");

            builder.Property(oi => oi.Price).HasColumnName("pricePerItem");

            builder.Property(oi => oi.OrdersId).HasColumnName("orderId");

            builder.Property(oi => oi.Quantity).HasColumnName("quantity");

            builder.Property(oi => oi.ProductVariantId).HasColumnName("productVariantId");

            builder.Property(oi => oi.TotalPrice).HasColumnName("totalPrice");

            builder.HasOne(oi => oi.Orders)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrdersId);

            builder.HasOne(oi => oi.ProductVariants)
                .WithMany(pv => pv.OrderItems)
                .HasForeignKey(oi => oi.ProductVariantId);
        }
    }
}
