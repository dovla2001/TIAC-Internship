using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.CartItem
{
    public class CartItemConfiguration : IEntityTypeConfiguration<Domain.Entities.CartItem>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.CartItem> builder)
        {
            builder.ToTable("cartItems");

            builder.HasKey(ci => ci.CartItemId);
            builder.Property(ci => ci.CartItemId).HasColumnName("cartItemId");

            builder.Property(ci => ci.Quantity).HasColumnName("quantity");

            builder.Property(ci => ci.Price).HasColumnName("price");

            builder.Property(ci => ci.TotalPrice).HasColumnName("totalPrice");

            builder.HasOne(ci => ci.Carts)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ci => ci.ProductVariant)
                .WithMany(pv => pv.CartItems)
                .HasForeignKey(ci => ci.ProductVariantId);

            builder.Property(ci => ci.CartId).HasColumnName("cartId");
            builder.Property(ci => ci.ProductVariantId).HasColumnName("productVariantId");
        }
    }
}
