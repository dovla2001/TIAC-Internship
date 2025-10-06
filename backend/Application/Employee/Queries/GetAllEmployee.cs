using Application.Employee.CommonEmployees;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Employee.Queries
{
    public class GetAllEmployee
    {
        public class GetAllEmployeeHandler : IRequestHandler<GetAllEmployeeQuery, List<Employees>>
        {
            private readonly IEmployeesRepository _employeesRepository;

            public GetAllEmployeeHandler(IEmployeesRepository employeesRepository)
            {
                _employeesRepository = employeesRepository;
            }

            public async Task<List<Employees>> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
            {
                var allEmployees = await _employeesRepository.GetAllEmployeesAsync(cancellationToken);

                return allEmployees;
            }
        }

        public record GetAllEmployeeQuery() : IRequest<List<Employees>>;
    }
}
