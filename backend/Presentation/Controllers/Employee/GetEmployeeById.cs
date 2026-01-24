using FastEndpoints;
using MediatR;
using Presentation.Contract.Employees;
using Presentation.Mappers;
using static Application.Employee.Queries.GetEmployeeById;

namespace Presentation.Controllers.Employee
{
    public class GetEmployeeById : EndpointWithoutRequest<ReadEmployeeResponse>
    {
        private readonly IMediator _mediator;

        public GetEmployeeById(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("employees/{employeeId}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken cancellationToken)
        {
            var employeeId = Route<int>("employeeId");
            var employee = await _mediator.Send(new GetEmployeeByIdQuery(employeeId));
            if (employee is null)
            {
                await SendNotFoundAsync(cancellationToken);
            }

            await SendOkAsync(employee.ToApiResponse(), cancellationToken);
        }
    }
}
