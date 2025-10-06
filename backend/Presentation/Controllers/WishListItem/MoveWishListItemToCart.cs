using Application.WishListItems.Command;
using FastEndpoints;
using MediatR;
using Presentation.Contract.WishListItem;
using System.Security.Claims;

namespace Presentation.Controllers.WishListItem
{
    public class MoveWishListItemToCart : Endpoint<MoveWishlistItemToCartRequest>
    {
        private readonly IMediator _mediator;

        public MoveWishListItemToCart(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Post("wishlist/move-to-cart");
        }

        public override async Task HandleAsync(MoveWishlistItemToCartRequest request, CancellationToken cancellationToken)
        {
            var employeeIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (employeeIdClaim is null || !int.TryParse(employeeIdClaim.Value, out var employeeId))
            {
                await SendUnauthorizedAsync(cancellationToken);
                return;
            }

            var command = new MoveWishListItemToCartCommand(request.WishlistItemId, employeeId);

            await _mediator.Send(command, cancellationToken);

            await SendNoContentAsync(cancellationToken);
        }
    }
}
