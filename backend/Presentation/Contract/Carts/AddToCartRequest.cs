namespace Presentation.Contract.Carts
{
    public class AddToCartRequest
    {
        public int ProductVariantId { get; set; }
        public int Quantity { get; set; }
    }
}
