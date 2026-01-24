using Application.Employee.CommonEmployees;
using Domain.Entities;
using MediatR;

namespace Application.Employee.Queries
{
    public class GetAllEmployeesQueryPaginated
    {
        public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, (List<Employees> Items, int totalCount)>
        {
            private readonly IEmployeesRepository _employeesRepository;

            public GetAllEmployeesQueryHandler(IEmployeesRepository employeesRepository)
            {
                _employeesRepository = employeesRepository;
            }

            public async Task<(List<Employees> Items, int totalCount)> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
            {
                var (employees, totalCount) = await _employeesRepository.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);

                return (employees, totalCount);
            }
        }

        public record GetAllEmployeesQuery(int PageNumber, int PageSize) : IRequest<(List<Employees> Items, int TotalCount)>; //da li ovo prebaciti u zasebnu klasu
    }
}
