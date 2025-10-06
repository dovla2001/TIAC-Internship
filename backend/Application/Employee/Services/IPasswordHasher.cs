using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Employee.Services
{
    public interface IPasswordHasher
    {
        public Task<string> HashPasswordAsync(string plainTextPassword, CancellationToken cancellationToken);
        public Task<bool> VerifyPasswordAsync(string plainTextPassword, string password, CancellationToken cancellationToken);
    }
}
