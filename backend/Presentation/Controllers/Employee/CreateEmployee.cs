using FastEndpoints;
using MediatR;
using Presentation.Contract.Employees;
using Presentation.Mappers;
using Presentation.Validators.Employee;

namespace Presentation.Controllers.Employee
{
    public class CreateEmployee : Endpoint<CreateEmployeeRequest, CreateEmployeeResponse>
    {
        private readonly IMediator _mediator;

        public CreateEmployee(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Post("employees");
            //AllowAnonymous();
            
            Validator<CreateEmployeeRequestValidator>();
        }

        public override async Task HandleAsync(CreateEmployeeRequest req, CancellationToken cancellationToken)
        {
            var employee = await _mediator.Send(req.ToCommand(), cancellationToken);
            await SendOkAsync(employee.ToApiResponseFromCommand(), cancellationToken);
        }
    }
}
