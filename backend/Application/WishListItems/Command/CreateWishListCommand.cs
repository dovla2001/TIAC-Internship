using Application.WishListItems.CommonWishListItem;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.WishListItems.Command
{
    public class AddWishListCommandHandler : IRequestHandler<CreateWishListCommand, int>
    {
        private readonly IWishListRepository _wishListRepository;

        public AddWishListCommandHandler(IWishListRepository wishListRepository)
        {
            _wishListRepository = wishListRepository;
        }

        public async Task<int> Handle(CreateWishListCommand request, CancellationToken cancellationToken)
        {
            var employeeId = request.employeeId;

            var itemExists = await _wishListRepository.ItemExistsAsync(employeeId, request.productVariantId, cancellationToken);

            if (itemExists)
            {
                throw new Exception("Item already in wishlist.");
            }

            var entity = new WishListItem
            {
                EmployeeId = request.employeeId,
                ProductVariantId = request.productVariantId,
                DateAdded = DateTime.UtcNow
            };

            await _wishListRepository.AddAsync(entity, cancellationToken);  

            return entity.WishListItemId;
        }
    }

    public record CreateWishListCommand(int productVariantId, int employeeId) : IRequest<int>;
}
