using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Employees
{
    public class EmployeesConfiguration : IEntityTypeConfiguration<Domain.Entities.Employees>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Employees> builder)
        {
            builder.ToTable("employees");

            builder.HasKey(x => x.EmployeesId);
            builder.Property(e => e.EmployeesId).HasColumnName("employeesId");

            builder.Property(e => e.FirstName).HasColumnName("firstName");

            builder.Property(e => e.LastName).HasColumnName("lastName");

            builder.Property(e => e.Email).HasColumnName("email");

            builder.Property(e => e.PasswordHash).HasColumnName("passwordHash");

            builder.Property(e => e.IsAdmin).HasColumnName("isAdmin");

            builder.Property(e => e.RefreshToken).HasColumnName("RefreshToken");

            builder.Property(e => e.RefreshTokenExpiryDate).HasColumnName("RefreshTokenExpiryDate");
        }
    }
}
