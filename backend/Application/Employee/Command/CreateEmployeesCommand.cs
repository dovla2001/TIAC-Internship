using Domain.Entities;
using MediatR;

namespace Application.Employee.Command
{
    public record CreateEmployeesCommand(string FirstName, string LastName, string Email, string Password) : IRequest<Employees>;
    public record DeleteEmployeesCommand(int employeeId) : IRequest<bool>;
    public record UpdateEmployeeCommand(int employeeId, string FirstName, string LastName, string Email) : IRequest<Employees>;
    public record RegisterEmployeeCommand(string FirstName, string LastName, string Email, string Password) : IRequest<Employees>;
    public record LoginEmployeeCommand(string Email, string Password) : IRequest<TokenResponse>;
    public record TokenResponse(string Token, string RefreshToken, bool IsAdmin); //bilo AccessToken, prebacio u Token
    public record RefreshTokenCommand(string RefreshToken) : IRequest<TokenResponse>;
}
