using Application.Employee.Command;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Employee.Services
{
    public interface IJwtTokenGenerator
    {
        public Task<string> GenerateToken(string email, CancellationToken cancellationToken);
    }
}
