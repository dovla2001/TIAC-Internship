using Application.WishListItems.Command;
using FastEndpoints;
using MediatR;
using Presentation.Contract.WishListItem;
using Presentation.Mappers;
using System.Security.Claims;

namespace Presentation.Controllers.WishListItem
{
    public class AddWishListItemEndpoint : Endpoint<AddWishlistItemRequest, AddWishlistItemResponse>
    {
        private readonly IMediator _mediator;

        public AddWishListItemEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Post("wishlist");
        }

        public override async Task HandleAsync(AddWishlistItemRequest req, CancellationToken cancellationToken)
        {
            var employeeIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (employeeIdClaim is null || !int.TryParse(employeeIdClaim.Value, out var employeeId))
            {
                await SendUnauthorizedAsync(cancellationToken);
                return;
            }

            var command = new CreateWishListCommand(req.ProductVariantId, employeeId);

            var newWishListItemId = await _mediator.Send(command, cancellationToken);

            var response = newWishListItemId.ToApiResponse();

            await SendOkAsync(response, cancellationToken);
        }
    }
}
