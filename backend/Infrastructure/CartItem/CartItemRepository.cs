using Application.CartsItem.CommonCartItem;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.CartItem
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly TiacWebShopDbContext _dbContext;

        public CartItemRepository(TiacWebShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Domain.Entities.CartItem> CreateCartItemAsync(Domain.Entities.CartItem carts, CancellationToken cancellationToken)
        {
            var newCartItem = await _dbContext.CartItems.AddAsync(carts, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return newCartItem.Entity;
        }

        public async Task DeleteCartItemAsync(Domain.Entities.CartItem carts, CancellationToken cancellationToken)
        {
            _dbContext.CartItems.Remove(carts);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Domain.Entities.CartItem?> GetByIdCartItemAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.CartItems.FirstOrDefaultAsync(ci => ci.CartItemId == id, cancellationToken);
        }

        public async Task UpdateCartItemAsync(Domain.Entities.CartItem carts, CancellationToken cancellationToken)
        {
            _dbContext.CartItems.Update(carts);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Domain.Entities.CartItem?> GetItemByVariantIdAsync(int cartId, int productVariantId, CancellationToken cancellationToken)
        {
            return await _dbContext.CartItems.FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.ProductVariantId == productVariantId, cancellationToken);
        }
    }
}
