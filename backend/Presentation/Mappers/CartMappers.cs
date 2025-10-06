using Domain.Entities;
using Presentation.Contract.Carts;

namespace Presentation.Mappers
{
    public static class CartMappers
    {
        public static CartResponseDto ToCartResponseDto(this Carts cart)
        {
            var cartDto = new CartResponseDto
            {
                CartId = cart.CartId,
                Items = cart.CartItems.Select(ci => new CartItemDto
                {
                    CartItemId = ci.CartItemId,
                    ProductVariantId = ci.ProductVariantId,
                    ProductName = ci.ProductVariant.Product.Name,
                    ImageUrl = ci.ProductVariant.Product.ImageUrl,
                    Quantity = ci.Quantity,
                    UnitPrice = ci.Price,
                    VariantDescription = string.Join(" / ", ci.ProductVariant.VariantValues
                        .Select(vv => $"{vv.AttributeValue.Attribute.Name}: {vv.AttributeValue.Value}"))
                }).ToList()
            };

            cartDto.GrandTotal = cartDto.Items.Sum(item => item.TotalPrice);
            return cartDto;
        }
    }
}
