using Application.Cart.CommonCarts;
using Application.CartsItem.CommonCartItem;
using Application.ProductVariant.CommonProductVariants;
using Domain.Entities;
using MediatR;

namespace Application.Cart.Command
{
    public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand>
    {
        private readonly ICartsRepository _cartsRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IProductsVariantsRepository _productsVariantsRepository;

        public AddToCartCommandHandler(ICartsRepository cartRepository, ICartItemRepository cartItemRepository, IProductsVariantsRepository productsVariantsRepository)
        {
            _cartsRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _productsVariantsRepository = productsVariantsRepository;
        }

        public async Task Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var activeCart = await _cartsRepository.GetActiveCartByEmployeeIdAsync(request.EmployeeId, cancellationToken);

            if (activeCart is null)
            {
                var newCart = new Carts
                {
                    EmployeesId = request.EmployeeId,
                    IsCartActive = true,
                    DateCreated = DateTime.UtcNow,
                    TotalPrice = 0
                };
                activeCart = await _cartsRepository.CreateCartsAsync(newCart, cancellationToken);
            }

            var existingItem = await _cartItemRepository.GetItemByVariantIdAsync(activeCart.CartId, request.ProductVariantId, cancellationToken);

            if (existingItem is not null)
            {
                existingItem.Quantity += request.Quantity;
                existingItem.TotalPrice = existingItem.Quantity * existingItem.Price;
                await _cartItemRepository.UpdateCartItemAsync(existingItem, cancellationToken);
            }
            else
            {
                var variant = await _productsVariantsRepository.GetByIdProductVariantsAsync(request.ProductVariantId, cancellationToken);
                if (variant is null)
                {
                    throw new Exception("Product variant not found.");
                }

                var newItem = new CartItem
                {
                    CartId = activeCart.CartId,
                    ProductVariantId = request.ProductVariantId,
                    Quantity = request.Quantity,
                    Price = variant.Price,
                    TotalPrice = request.Quantity * variant.Price
                };
                await _cartItemRepository.CreateCartItemAsync(newItem, cancellationToken);
            }

            var updatedCartWithItems = await _cartsRepository.GetActiveCartDetailsAsync(request.EmployeeId, cancellationToken);

            if (updatedCartWithItems is null)
            {
                throw new Exception("Error reloading carte.");
            }

            var newTotalPrice = updatedCartWithItems.CartItems.Sum(item => item.TotalPrice);

            updatedCartWithItems.TotalPrice = newTotalPrice;

            await _cartsRepository.UpdateCartsAsync(updatedCartWithItems, cancellationToken);
        }
    }

    public record AddToCartCommand(int ProductVariantId, int Quantity, int EmployeeId) : IRequest;
}
