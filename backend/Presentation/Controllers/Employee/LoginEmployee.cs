using Application.Employee.Command;
using FastEndpoints;
using MediatR;
using Presentation.Contract.Employees;
using Presentation.Mappers;
using Presentation.Validators.Employee;

namespace Presentation.Controllers.Employee
{
    public class LoginEmployee : Endpoint<LoginEmployeeRequest, LoginEmployeeResponse>
    {
        private readonly IMediator _mediator;

        public LoginEmployee(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Post("/auth/login");
            AllowAnonymous();
            Validator<LoginEmployeeRequestValidator>();
        }

        public override async Task HandleAsync(LoginEmployeeRequest request, CancellationToken cancellationToken)
        {
            var command = new LoginEmployeeCommand(request.Email, request.Password);
            var response = await _mediator.Send(command, cancellationToken);
            await SendOkAsync(response.ToApiResponse(), cancellationToken);
        }
    }
}
