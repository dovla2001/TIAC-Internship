using Application.Cart.Command;
using Application.WishListItems.CommonWishListItem;
using MediatR;

namespace Application.WishListItems.Command
{
    public class MoveWishListItemToCartCommandHandler : IRequestHandler<MoveWishListItemToCartCommand>
    {
        private readonly IMediator _mediator;
        private readonly IWishListRepository _wishListRepository;

        public MoveWishListItemToCartCommandHandler(IMediator mediator, IWishListRepository wishListRepository)
        {
            _mediator = mediator;
            _wishListRepository = wishListRepository;
        }

        public async Task Handle(MoveWishListItemToCartCommand request, CancellationToken cancellationToken)
        {
            var wishListItem = await _wishListRepository.GetByIdAndUserAsync(request.wishListItemId, request.employeeId, cancellationToken);

            if (wishListItem is null)
            {
                throw new Exception("WishList item not found!");
            }

            var addToCartCommand = new AddToCartCommand(wishListItem.ProductVariantId, 1, request.employeeId);

            await _mediator.Send(addToCartCommand, cancellationToken);

            await _wishListRepository.DeleteAsync(wishListItem.WishListItemId, wishListItem.EmployeeId, cancellationToken);
        }
    }

    public record MoveWishListItemToCartCommand(int wishListItemId, int employeeId) : IRequest;
}
