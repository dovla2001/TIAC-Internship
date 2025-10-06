using Application.Order.Command;
using FastEndpoints;
using MediatR;
using Presentation.Contract.Order;
using System.Security.Claims;

namespace Presentation.Controllers.Order
{
    public class CreateOrder : EndpointWithoutRequest<CreateOrderResponse>
    {
        private readonly IMediator _mediator;

        public CreateOrder(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Post("orders");
            Roles("Employee");
        }

        public override async Task HandleAsync(CancellationToken cancellationToken)
        {
            var employeeIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(employeeIdString, out var employeeId))
            {
                await SendUnauthorizedAsync(cancellationToken);
                return;
            }

            var command = new CreateOrderFromCartCommand(employeeId);

            var newOrderId = await _mediator.Send(command, cancellationToken);

            var response = new CreateOrderResponse
            {
                OrderId = newOrderId
            };

            await SendOkAsync(response, cancellationToken);
        }
    }
}
