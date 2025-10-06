using FastEndpoints;
using MediatR;
using Presentation.Contract.WishListItem;
using Presentation.Mappers;
using System.Security.Claims;
using static Application.WishListItems.Queries.GetMyWishList;

namespace Presentation.Controllers.WishListItem
{
    public class GetMyWishListEndpoint : EndpointWithoutRequest<List<WishListItemResponse>>
    {
        private readonly IMediator _medator;

        public GetMyWishListEndpoint(IMediator medator)
        {
            _medator = medator;
        }

        public override void Configure()
        {
            Get("wishlist/details");
        }

        public override async Task HandleAsync(CancellationToken cancellationToken)
        {
            var employeeIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (employeeIdClaim is null || !int.TryParse(employeeIdClaim.Value, out var employeeId))
            {
                await SendUnauthorizedAsync(cancellationToken);
                return;
            }

            var query = new GetMyWishListQuery(employeeId);

            var wishListResponse = await _medator.Send(query, cancellationToken);

            var response = wishListResponse.ToApiResponse();

            await SendOkAsync(response, cancellationToken);
        }
    }
}
