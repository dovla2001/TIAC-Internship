using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Employee.CommonEmployees;
using System.Linq.Expressions;

namespace Application.Employee.Queries
{
    public class GetEmployeeById
    {
        public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, Employees>
        {
            private readonly IEmployeesRepository _employeesRepository;

            public GetEmployeeByIdHandler(IEmployeesRepository employeesRepository)
            {
                _employeesRepository = employeesRepository;
            }

            public async Task<Employees> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
            {
                var employee = await _employeesRepository.GetByIdEmployeesAsync(request.Id, cancellationToken);
                if (employee is null)
                {
                    throw new Exception($"Employee with id {request.Id} doesn't exists!");
                }
                
                return employee;
            }
        }

        public record GetEmployeeByIdQuery(int Id) : IRequest<Employees>;
    }
}
