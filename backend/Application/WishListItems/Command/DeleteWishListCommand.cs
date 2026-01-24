using Application.WishListItems.CommonWishListItem;
using MediatR;

namespace Application.WishListItems.Command
{
    public class DeleteWishListCommandHandler : IRequestHandler<DeleteWishListItemCommand>
    {
        private IWishListRepository _wishListRepository;

        public DeleteWishListCommandHandler(IWishListRepository wishListRepository)
        {
            _wishListRepository = wishListRepository;
        }

        public async Task Handle(DeleteWishListItemCommand request, CancellationToken cancellationToken)
        {
            var deletedWishItem = await _wishListRepository.DeleteAsync(request.wishListItemId, request.employeeId, cancellationToken);

            if (!deletedWishItem)
            {
                throw new Exception("Wishlist item not found or you do not have permission to delete it.");
            }
        }
    }

    public record DeleteWishListItemCommand(int wishListItemId, int employeeId) : IRequest;
}
