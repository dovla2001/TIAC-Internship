using Application.Employee.CommonEmployees;
using Application.Employee.Mappers;
using Domain.Entities;
using MediatR;

namespace Application.Employee.Command
{
    public class CreateEmployeesCommandHandler : IRequestHandler<CreateEmployeesCommand, Employees>
    {
        private readonly IEmployeesRepository _employeesRepository;

        public CreateEmployeesCommandHandler(IEmployeesRepository employeesRepository)
        {
            _employeesRepository = employeesRepository;
        }

        public async Task<Employees> Handle(CreateEmployeesCommand request, CancellationToken cancellationToken)
        {
            var existingEmployee = await _employeesRepository.GetByEmailEmployeesAsync(request.Email, cancellationToken);
            if (existingEmployee is not null)
            {
                throw new Exception($"Employee with email {request.Email} already exists!");
            }

            var domainEntity = request.ToDomainEntity();
            var persistedEmployee = await _employeesRepository.CreateEmployeesAsync(domainEntity, cancellationToken);

            return persistedEmployee;
        }
    }
}
