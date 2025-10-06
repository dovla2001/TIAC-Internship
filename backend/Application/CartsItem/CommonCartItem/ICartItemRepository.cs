using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CartsItem.CommonCartItem
{
    public interface ICartItemRepository
    {
        public Task<CartItem?> GetByIdCartItemAsync(int id, CancellationToken cancellationToken);
        public Task<CartItem> CreateCartItemAsync(CartItem carts, CancellationToken cancellationToken);
        public Task UpdateCartItemAsync(CartItem carts, CancellationToken cancellationToken);
        public Task DeleteCartItemAsync(CartItem carts, CancellationToken cancellationToken);

        public Task<CartItem?> GetItemByVariantIdAsync(int cartId, int productVariantId, CancellationToken cancellationToken);
        
    }
}
