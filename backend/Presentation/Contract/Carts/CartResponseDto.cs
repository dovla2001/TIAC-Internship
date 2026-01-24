namespace Presentation.Contract.Carts
{

    public class CartItemDto
    {
        public int CartItemId { get; set; }
        public int ProductVariantId { get; set; }
        public string ProductName { get; set; }
        public string VariantDescription { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;
        public string? ImageUrl { get; set; }

    }

    public class CartResponseDto
    {
        public int CartId { get; set; }
        public List<CartItemDto> Items { get; set; } = new();
        public decimal GrandTotal { get; set; }
    }
}
