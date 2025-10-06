using Application.Employee.Command;
using Application.Employee.CommonEmployees;
using Application.Employee.Services;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Employees.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private IEmployeesRepository _employeesRepository;
        private IConfiguration _configuration;

        public JwtTokenGenerator(IConfiguration configuration, IEmployeesRepository employeesRepository)
        {
            _configuration = configuration;
            _employeesRepository = employeesRepository;
        }

        public async Task<string> GenerateToken(string email, CancellationToken cancellationToken)
        {

            var employee = await _employeesRepository.GetByEmailEmployeesAsync(email, cancellationToken);

            if (employee is null)
            {
                throw new ArgumentException($"The user with email {email} does not exist!");
            }

            var jwtSettings = _configuration.GetSection("JWT");
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, employee.EmployeesId.ToString()), 
                new(JwtRegisteredClaimNames.Email, employee.Email),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), 
                new(ClaimTypes.Name, $"{employee.FirstName} {employee.LastName}"), 
                new(ClaimTypes.Role, employee.IsAdmin ? "Admin" : "Employee") 
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256));

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            
            return tokenString;
        }
    }
}
