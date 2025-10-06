using Application.Employee.Command;
using FastEndpoints;
using MediatR;
using Presentation.Contract.Employees;
using Presentation.Validators.Employee;
using System.Linq.Expressions;

namespace Presentation.Controllers.Employee
{
    public class UpdateEmployee : Endpoint<UpdateEmployeeRequest, UpdateEmployeeResponse>
    {
        private readonly IMediator _mediator;

        public UpdateEmployee(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Put("employees/{employeeIdFromRoute}");
            //AllowAnonymous();
            Validator<UpdateEmployeeRequestValidator>();
        }

        public override async Task HandleAsync(UpdateEmployeeRequest req, CancellationToken cancellationToken)
        {
            var employeeId = Route<int>("employeeIdFromRoute");
            if (employeeId != req.EmployeeId)
            {
                await SendResultAsync(Results.BadRequest("Ids doesn't match!"));
                return;
            }
            var command = new UpdateEmployeeCommand(req.EmployeeId, req.FirstName, req.LastName, req.Email);
            var wasUpdated = await _mediator.Send(command, cancellationToken);

            var response = new UpdateEmployeeResponse
            {
                EmployeeId = wasUpdated.EmployeesId,
                FirstName = req.FirstName,
                LastName = req.LastName,
                Email = req.Email
            };

            await SendOkAsync(response, cancellationToken);
        }
    }
}
