using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Employee.CommonEmployees;

namespace Application.Employee.Command
{
    public class DeleteEmployeesCommandHandler : IRequestHandler<DeleteEmployeesCommand, bool>
    {
        private readonly IEmployeesRepository _employeesRepository;

        public DeleteEmployeesCommandHandler(IEmployeesRepository employeesRepository)
        {
            _employeesRepository = employeesRepository;
        }

        public async Task<bool> Handle(DeleteEmployeesCommand request, CancellationToken cancellationToken)
        {
            var existingEmployee = await _employeesRepository.GetByIdEmployeesAsync(request.employeeId, cancellationToken);
            if ( existingEmployee is null)
            {
                throw new Exception("This user doesn't exist!");
            }

            await _employeesRepository.DeleteEmployeesAsync(existingEmployee, cancellationToken);

            return true;
        }
    }
}
