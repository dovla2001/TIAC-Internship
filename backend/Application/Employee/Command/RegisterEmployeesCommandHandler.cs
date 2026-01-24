using Application.Employee.CommonEmployees;
using Application.Employee.Mappers;
using Application.Employee.Services;
using Application.Exceptions;
using Domain.Entities;
using MediatR;

namespace Application.Employee.Command
{
    public class RegisterEmployeesCommandHandler : IRequestHandler<RegisterEmployeeCommand, Employees>
    {
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterEmployeesCommandHandler(IEmployeesRepository employeesRepository, IPasswordHasher passwordHasher)
        {
            _employeesRepository = employeesRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Employees> Handle(RegisterEmployeeCommand request, CancellationToken cancellationToken)
        {
            var domainEntity = request.ToDomainEntity();
            var existingEmployee = await _employeesRepository.GetByEmailEmployeesAsync(request.Email, cancellationToken);
            if (existingEmployee is not null)
            {
                throw new DuplicateEmailException("Employee with this email already exists!");
            }

            domainEntity.PasswordHash = await _passwordHasher.HashPasswordAsync(request.Password, cancellationToken);
            var persistedEmployee = await _employeesRepository.CreateEmployeesAsync(domainEntity, cancellationToken);
            return persistedEmployee;
        }
    }
}
