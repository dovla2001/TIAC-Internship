using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Orders
{
    public class OrdersConfiguration : IEntityTypeConfiguration<Domain.Entities.Orders>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Orders> builder)
        {
            builder.ToTable("orders");

            builder.HasKey(o => o.OrdersId);

            builder.Property(o => o.OrdersId).HasColumnName("ordersId");

            builder.Property(o => o.EmployeeId).HasColumnName("employeeId");

            builder.Property(o => o.OrderDate).HasColumnName("orderDate");

            builder.Property(o => o.TotalPrice).HasColumnName("totalPrice");

            builder.HasOne(o => o.Employees).
                    WithMany(employee => employee.Orders).
                    HasForeignKey(o => o.EmployeeId);
        }
    }
}
