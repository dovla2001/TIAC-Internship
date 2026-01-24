using FastEndpoints;
using MediatR;
using Presentation.Common;
using Presentation.Contract.Employees;
using static Application.Employee.Queries.GetAllEmployeesQueryPaginated;

namespace Presentation.Controllers.Employee
{
    public class GetAllEmployee : Endpoint<GetAllEmployeesRequest, PagedList<ReadEmployeeResponse>>
    {
        private readonly IMediator _mediator;

        public GetAllEmployee(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("/employees/getAll");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetAllEmployeesRequest request, CancellationToken cancellationToken)
        {
            var pageNumber = request.PageNumber ?? 1;
            var pageSize = request.PageSize ?? 2;

            var query = new GetAllEmployeesQuery(pageNumber, pageSize);

            var (employeeEntites, totalCount) = await _mediator.Send(query, cancellationToken);

            var response = employeeEntites.Select(e => new ReadEmployeeResponse
            {
                Id = e.EmployeesId,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email
            }).ToList();

            var pagedResponse = new PagedList<ReadEmployeeResponse>(
               response,
               pageNumber,
               pageSize,
               totalCount);

            await SendOkAsync(pagedResponse, cancellationToken);
        }
    }
}
