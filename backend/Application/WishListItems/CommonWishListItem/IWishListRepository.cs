using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.WishListItems.CommonWishListItem
{
    public interface IWishListRepository
    {
        public Task AddAsync(WishListItem item, CancellationToken cancellationToken);

        public Task<bool> ItemExistsAsync(int employeeId, int productVariantId, CancellationToken cancellationToken);

        public Task<List<WishListItem>> GetItemsForUserAsync(int employeeId, CancellationToken cancellationToken);

        public Task<bool> DeleteAsync(int wishListItemId, int employeeId, CancellationToken cancellationToken);

        public Task<WishListItem?> GetByIdAndUserAsync(int wishListItemId, int employeeId, CancellationToken cancellationToken);
    }
}
