using Application.Employee.CommonEmployees;
using Domain.Entities;
using MediatR;

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
