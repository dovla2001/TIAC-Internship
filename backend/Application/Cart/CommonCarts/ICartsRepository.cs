using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cart.CommonCarts
{
    public interface ICartsRepository
    {
        public Task<Carts?> GetByIdCartsAsync(int id, CancellationToken cancellationToken);
        public Task<Carts> CreateCartsAsync(Carts carts, CancellationToken cancellationToken);
        public Task UpdateCartsAsync(Carts carts, CancellationToken cancellationToken);
        public Task DeleteCartsAsync(Carts carts, CancellationToken cancellationToken);

        public Task<Carts?> GetActiveCartByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken);
        public Task<Carts?> GetActiveCartDetailsAsync(int employeeId, CancellationToken cancellationToken);
        public Task RemoveCartItemAsync(CartItem cartItem, CancellationToken cancellationToken);
        public Task<CartItem?> GetCartItemByIdAsync(int cartItemId, CancellationToken cancellationToken);
    }
}
