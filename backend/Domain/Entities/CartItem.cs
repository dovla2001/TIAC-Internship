namespace Domain.Entities
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public Carts Carts { get; set; }
        public int ProductVariantId { get; set; }
        public ProductVariants ProductVariant { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
