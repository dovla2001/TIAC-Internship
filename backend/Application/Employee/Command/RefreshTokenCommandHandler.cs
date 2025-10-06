using Application.Employee.CommonEmployees;
using Application.Employee.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Employee.Command
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenResponse>
    {
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public RefreshTokenCommandHandler(IEmployeesRepository employeesRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _employeesRepository = employeesRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<TokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeesRepository.GetByRefreshTokenAsync(request.RefreshToken, cancellationToken);

            if (employee is null || employee.RefreshTokenExpiryDate <= DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Refresh token is invalid or expired.");
            }

            var newAccessToken = await _jwtTokenGenerator.GenerateToken(employee.Email, cancellationToken);

            var newRefreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            employee.RefreshToken = newRefreshToken;
            employee.RefreshTokenExpiryDate = DateTime.UtcNow.AddDays(7);
            await _employeesRepository.UpdateEmployeesAsync(employee, cancellationToken);

            return new TokenResponse(newAccessToken, newRefreshToken, employee.IsAdmin);
        }
    }
}
