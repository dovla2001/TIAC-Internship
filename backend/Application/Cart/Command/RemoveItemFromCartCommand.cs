using Application.Cart.CommonCarts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cart.Command
{
    public class RemoveItemFromCartCommandHandler : IRequestHandler<RemoveItemFromCartCommand, bool>
    {
        private ICartsRepository _cartRepository;

        public RemoveItemFromCartCommandHandler(ICartsRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<bool> Handle(RemoveItemFromCartCommand request, CancellationToken cancellationToken)
        {
            var cartItem = await _cartRepository.GetCartItemByIdAsync(request.cartItemId, cancellationToken);

            if (cartItem is null)
            {
                throw new Exception("Cart item doesn't exist!");
            }

            await _cartRepository.RemoveCartItemAsync(cartItem, cancellationToken);

            var updatedCart = await _cartRepository.GetActiveCartDetailsAsync(request.employeeId, cancellationToken);

            if (updatedCart != null)
            {
                updatedCart.TotalPrice = updatedCart.CartItems.Sum(item => item.TotalPrice);

                await _cartRepository.UpdateCartsAsync(updatedCart, cancellationToken);
            }

            return true;
        }
    }

    public record RemoveItemFromCartCommand(int cartItemId, int employeeId) : IRequest<bool>;
}
