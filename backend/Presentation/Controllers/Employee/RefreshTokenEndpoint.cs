using Application.Employee.Command;
using FastEndpoints;
using MediatR;
using Presentation.Contract.Employees;

namespace Presentation.Controllers.Employee
{
    public class RefreshTokenEndpoint : Endpoint<RefreshTokenRequest, TokenResponse>
    {
        private readonly IMediator _mediator;

        public RefreshTokenEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Post("auth/refresh");
            AllowAnonymous();
        }

        public override async Task HandleAsync(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var command = new RefreshTokenCommand(request.RefreshToken);
            var response = await _mediator.Send(command, cancellationToken);
;       
            await SendOkAsync(response, cancellationToken);
        }
    }
}
