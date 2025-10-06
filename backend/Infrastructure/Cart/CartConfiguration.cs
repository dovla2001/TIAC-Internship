using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Cart
{
    public class CartConfiguration : IEntityTypeConfiguration<Domain.Entities.Carts>
    {
        public void Configure(EntityTypeBuilder<Carts> builder)
        {
            builder.ToTable("carts");

            builder.HasKey(c => c.CartId);
            builder.Property(c => c.CartId).HasColumnName("cartId");

            builder.Property(c => c.EmployeesId).HasColumnName("employeeId");

            builder.Property(c => c.DateCreated).HasColumnName("dateCreated");

            builder.Property(c => c.IsCartActive).HasColumnName("isCartActive");

            builder.Property(c => c.TotalPrice).HasColumnName("totalPrice");

            builder.HasOne(c => c.Employees)
                .WithMany(e => e.Carts)
                .HasForeignKey(c => c.EmployeesId);
        }
    }
}
