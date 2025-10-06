using Application.Employee.CommonEmployees;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Employee.Command
{
    public class UpdateEmployeesCommandHandler : IRequestHandler<UpdateEmployeeCommand, Employees>
    {
        private readonly IEmployeesRepository _employeesRepository;

        public UpdateEmployeesCommandHandler(IEmployeesRepository employeesRepository)
        {
            _employeesRepository = employeesRepository;
        }

        public async Task<Employees> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var existingEmployee = await _employeesRepository.GetByIdEmployeesAsync(request.employeeId, cancellationToken);
            if (existingEmployee is null)
            {
                throw new Exception("This employee doesn't exist!");
            }

            existingEmployee.FirstName = request.FirstName;
            existingEmployee.LastName = request.LastName;
            existingEmployee.Email = request.Email;

            await _employeesRepository.UpdateEmployeesAsync(existingEmployee, cancellationToken);

            return existingEmployee;
        }
    }
}
