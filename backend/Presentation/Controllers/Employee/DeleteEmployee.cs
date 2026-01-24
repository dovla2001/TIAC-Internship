using Application.Employee.Command;
using FastEndpoints;
using MediatR;
using Presentation.Contract.Employees;

namespace Presentation.Controllers.Employee
{
    public class DeleteEmployee : EndpointWithoutRequest<DeleteEmployeeResponse>
    {
        private readonly IMediator _mediator;

        public DeleteEmployee(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Delete("employees/{employeeId}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken cancellationToken)
        {
            var employeeId = Route<int>("employeeId");
            var wasDeleted = await _mediator.Send(new DeleteEmployeesCommand(employeeId), cancellationToken);

            if (!wasDeleted)
            {
                await SendNotFoundAsync(cancellationToken);
            }

            await SendNoContentAsync(cancellationToken);

        }
    }
}
