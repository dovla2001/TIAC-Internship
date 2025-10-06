using Application.Employee.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Employees.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public Task<string> HashPasswordAsync(string plainTextPassword, CancellationToken cancellationToken)
        {
            return Task.FromResult(BCrypt.Net.BCrypt.HashPassword(plainTextPassword));
        }

        public Task<bool> VerifyPasswordAsync(string plainTextPassword, string password, CancellationToken cancellationToken)
        {
            return Task.FromResult(BCrypt.Net.BCrypt.Verify(plainTextPassword, password));
        }
    }
}
