using Application.Cart.Command;
using FastEndpoints;
using MediatR;
using Presentation.Contract.Carts;
using System.Security.Claims;

namespace Presentation.Controllers.Cart
{
    public class AddToCart : Endpoint<AddToCartRequest>
    {
        private readonly IMediator _mediator;

        public AddToCart(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Post("/cart/items");
            //AllowAnonymous();
            Roles("Employee");
        }

        public override async Task HandleAsync(AddToCartRequest request, CancellationToken cancellationToken)
        {
            var employeeId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var command = new AddToCartCommand(request.ProductVariantId, request.Quantity, employeeId);

            await _mediator.Send(command, cancellationToken);

            await SendOkAsync(cancellationToken);
        }
    }
}
