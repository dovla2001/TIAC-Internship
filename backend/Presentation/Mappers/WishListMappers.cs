using Domain.Entities;
using Presentation.Contract.WishListItem;

namespace Presentation.Mappers
{
    public static class WishListMappers
    {
        public static AddWishlistItemResponse ToApiResponse(this int wishlistItemId)
        {
            return new AddWishlistItemResponse
            {
                WishlistItemId = wishlistItemId
            };
        }

        public static List<WishListItemResponse> ToApiResponse(this List<WishListItem> items)
        {
            return items.Select(item => new WishListItemResponse
            {
                WishListItemID = item.WishListItemId,
                ProductVariantId = item.ProductVariantId,
                ProductName = item.ProductVariant.Product.Name,
                Price = item.ProductVariant.Price,
                DateAdded = item.DateAdded,
                Attributes = item.ProductVariant.VariantValues
                        .ToDictionary(
                            vv => vv.AttributeValue.Attribute.Name,
                            vv => vv.AttributeValue.Value
                        )
            }).ToList();
        }
    }
}
