using Application.Employee.CommonEmployees;
using Application.Employee.Services;
using MediatR;
using System.Security.Authentication;
using System.Security.Cryptography;

namespace Application.Employee.Command
{
    public class LoginEmployeeCommandHandler : IRequestHandler<LoginEmployeeCommand, TokenResponse>
    {
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginEmployeeCommandHandler(IEmployeesRepository employeesRepository, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
        {
            _employeesRepository = employeesRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<TokenResponse> Handle(LoginEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeesRepository.GetByEmailEmployeesAsync(request.Email, cancellationToken);
            if (employee is null ||
                !await _passwordHasher.VerifyPasswordAsync(request.Password, employee.PasswordHash, cancellationToken))
            {
                throw new InvalidCredentialException("The email or password is incorrect!");
            }

            //access token
            var token = await _jwtTokenGenerator.GenerateToken(employee.Email, cancellationToken);

            //refresh token
            var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            employee.RefreshToken = refreshToken;
            employee.RefreshTokenExpiryDate = DateTime.UtcNow.AddDays(7);

            await _employeesRepository.UpdateEmployeesAsync(employee, cancellationToken);

            return new TokenResponse(token, refreshToken, employee.IsAdmin);
        }
    }
}
