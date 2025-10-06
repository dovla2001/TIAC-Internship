using Application.WishListItems.Command;
using FastEndpoints;
using MediatR;
using Presentation.Contract.WishListItem;
using System.Security.Claims;

namespace Presentation.Controllers.WishListItem
{
    public class DeleteWishListItemEndpoint : Endpoint<DeleteWishlistItemRequest>
    {
        private readonly IMediator _mediator;

        public DeleteWishListItemEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Delete("wishlist/{WishListItemId}");
        }

        public override async Task HandleAsync(DeleteWishlistItemRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var employeeId))
            {
                await SendUnauthorizedAsync(cancellationToken);
                return;
            }

            var command = new DeleteWishListItemCommand(request.WishListItemId, employeeId);

            await _mediator.Send(command, cancellationToken);

            await SendNoContentAsync(cancellationToken);
        }
    }
}
