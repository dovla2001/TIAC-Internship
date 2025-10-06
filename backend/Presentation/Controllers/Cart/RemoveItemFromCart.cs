using Application.Cart.Command;
using FastEndpoints;
using MediatR;
using System.Security.Claims;

namespace Presentation.Controllers.Cart
{
    public class RemoveItemFromCart : EndpointWithoutRequest
    {
        private readonly IMediator _mediator;

        public RemoveItemFromCart(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Delete("cart/{cartItemId}");
            Roles("Employee");
        }

        public override async Task HandleAsync(CancellationToken cancellationToken)
        {
            var cartItemId = Route<int>("cartItemId");

            var userIdString = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var command = new RemoveItemFromCartCommand(cartItemId, userIdString);

            var result = await _mediator.Send(command, cancellationToken);
        }
    }
}
