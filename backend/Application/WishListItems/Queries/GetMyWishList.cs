using Application.WishListItems.CommonWishListItem;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.WishListItems.Queries
{
    public class GetMyWishList
    {
        public class GetMyWishListHandler : IRequestHandler<GetMyWishListQuery, List<Domain.Entities.WishListItem>>
        {
            private readonly IWishListRepository _wishlListRepository;

            public GetMyWishListHandler(IWishListRepository wishlListRepository)
            {
                _wishlListRepository = wishlListRepository;
            }

            public async Task<List<WishListItem>> Handle(GetMyWishListQuery request, CancellationToken cancellationToken)
            {
                var employeeId = request.employeeId;

                var items = await _wishlListRepository.GetItemsForUserAsync(employeeId, cancellationToken);

                return items;
            }
        }

        public record GetMyWishListQuery(int employeeId) : IRequest<List<Domain.Entities.WishListItem>>;
    }
}
