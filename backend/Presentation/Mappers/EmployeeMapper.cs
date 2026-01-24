using Application.Employee.Command;
using Domain.Entities;
using Presentation.Contract.Employees;

namespace Presentation.Mappers
{
    public static class EmployeeMapper
    {
        public static LoginEmployeeResponse ToApiResponse(this TokenResponse tokenResponse) => new LoginEmployeeResponse(tokenResponse.Token, tokenResponse.RefreshToken, tokenResponse.IsAdmin);

        public static ReadEmployeeResponse ToApiResponse(this Employees employees)
        {
            return new ReadEmployeeResponse
            {
                Id = employees.EmployeesId,
                FirstName = employees.FirstName,
                LastName = employees.LastName,
                Email = employees.Email
            };
        }

        public static CreateEmployeesCommand ToCommand(this CreateEmployeeRequest request) => new CreateEmployeesCommand(request.FirstName, request.LastName, request.Email, request.Password);

        public static CreateEmployeeResponse ToApiResponseFromCommand(this Employees employees)
        {
            return new CreateEmployeeResponse
            {
                Id = employees.EmployeesId,
                FirstName = employees.FirstName,
                LastName = employees.LastName,
                Email = employees.Email,
            };
        }
    }
}
