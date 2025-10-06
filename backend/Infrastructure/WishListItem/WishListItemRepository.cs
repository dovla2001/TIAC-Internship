using Application.WishListItems.CommonWishListItem;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.WishListItem
{
    public class WishListItemRepository : IWishListRepository
    {
        private readonly TiacWebShopDbContext _dbContext;

        public WishListItemRepository(TiacWebShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Domain.Entities.WishListItem item, CancellationToken cancellationToken)
        {
            await _dbContext.AddAsync(item, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public Task<bool> ItemExistsAsync(int employeeId, int productVariantId, CancellationToken cancellationToken)
        {
            return _dbContext.WishListItems.AnyAsync(wi => wi.EmployeeId == employeeId && wi.ProductVariantId == productVariantId, cancellationToken);
        }

        public async Task<List<Domain.Entities.WishListItem>> GetItemsForUserAsync(int employeeId, CancellationToken cancellationToken)
        {
            return await _dbContext.WishListItems
             .Where(wi => wi.EmployeeId == employeeId)
             .Include(wi => wi.ProductVariant)
                 .ThenInclude(pv => pv.Product)
             .Include(wi => wi.ProductVariant)
                 .ThenInclude(pv => pv.VariantValues)        
                     .ThenInclude(vv => vv.AttributeValue)   
                         .ThenInclude(av => av.Attribute)    
             .OrderByDescending(wi => wi.DateAdded)
             .ToListAsync(cancellationToken);
            }

        public async Task<bool> DeleteAsync(int wishListItemId, int employeeId, CancellationToken cancellationToken)
        {
            var itemToDelete = await _dbContext.WishListItems.FirstOrDefaultAsync(wi => wi.WishListItemId ==  wishListItemId && wi.EmployeeId == employeeId, cancellationToken);    

            if (itemToDelete is null)
            {
                return false;
            }

            _dbContext.WishListItems.Remove(itemToDelete);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<Domain.Entities.WishListItem?> GetByIdAndUserAsync(int wishListItemId, int employeeId, CancellationToken cancellationToken)
        {
            return await _dbContext.WishListItems.FirstOrDefaultAsync(wi => wi.WishListItemId == wishListItemId && wi.EmployeeId == employeeId, cancellationToken);
        }
    }
}
