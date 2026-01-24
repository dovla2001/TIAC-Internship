using Application.Employee.Services;

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
