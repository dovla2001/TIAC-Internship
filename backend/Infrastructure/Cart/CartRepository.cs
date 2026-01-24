using Application.Cart.CommonCarts;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Cart
{
    public class CartRepository : ICartsRepository
    {
        private readonly TiacWebShopDbContext _dbContext;

        public CartRepository(TiacWebShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Carts> CreateCartsAsync(Carts carts, CancellationToken cancellationToken)
        {
            var newCarts = await _dbContext.Carts.AddAsync(carts, cancellationToken);
            await _dbContext.SaveChangesAsync();
            return newCarts.Entity;
        }

        public async Task DeleteCartsAsync(Carts carts, CancellationToken cancellationToken)
        {
            _dbContext.Carts.Remove(carts);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Carts?> GetByIdCartsAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Carts.FirstOrDefaultAsync(c => c.CartId == id, cancellationToken);
        }

        public async Task UpdateCartsAsync(Carts carts, CancellationToken cancellationToken)
        {
            _dbContext.Carts.Update(carts);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Carts?> GetActiveCartByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken)
        {
            return await _dbContext.Carts.FirstOrDefaultAsync(c => c.EmployeesId == employeeId && c.IsCartActive, cancellationToken);
        }

        public async Task<Carts?> GetActiveCartDetailsAsync(int employeeId, CancellationToken cancellationToken)
        {
            return await _dbContext.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.ProductVariant)
                    .ThenInclude(pv => pv.Product)
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.ProductVariant)
                    .ThenInclude(pv => pv.VariantValues)
                    .ThenInclude(vv => vv.AttributeValue)
                    .ThenInclude(av => av.Attribute)
                .FirstOrDefaultAsync(c => c.EmployeesId == employeeId && c.IsCartActive, cancellationToken);
        }

        public async Task RemoveCartItemAsync(Domain.Entities.CartItem cartItem, CancellationToken cancellationToken)
        {
            _dbContext.CartItems.Remove(cartItem);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Domain.Entities.CartItem?> GetCartItemByIdAsync(int cartItemId, CancellationToken cancellationToken)
        {
            return await _dbContext.CartItems.Include(ci => ci.Carts).FirstOrDefaultAsync(item => item.CartItemId == cartItemId, cancellationToken);
        }
    }
}
