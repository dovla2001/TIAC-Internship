using Application.Employee.Command;
using FastEndpoints;
using MediatR;
using Presentation.Contract.Employees;
using Presentation.Mappers;
using Presentation.Validators.Employee;

namespace Presentation.Controllers.Employee
{
    public class RegisterEmployee : Endpoint<CreateEmployeeRequest, CreateEmployeeResponse>
    {
        private readonly IMediator _mediator;

        public RegisterEmployee(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Post("auth/register");
            AllowAnonymous();
            Validator<CreateEmployeeRequestValidator>();
        }

        public override async Task HandleAsync(CreateEmployeeRequest request, CancellationToken cancellationToken)
        {
            var command = new RegisterEmployeeCommand(request.FirstName, request.LastName, request.Email, request.Password);
            var registerEmployee = await _mediator.Send(command, cancellationToken);
            await SendOkAsync(registerEmployee.ToApiResponseFromCommand(), cancellationToken);
        }
    }
}
