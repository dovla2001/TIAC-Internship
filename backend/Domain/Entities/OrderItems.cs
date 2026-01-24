namespace Domain.Entities
{
    public class OrderItems
    {
        public int OrderItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Orders Orders { get; set; }
        public int OrdersId { get; set; }
        public int ProductVariantId { get; set; }
        public decimal TotalPrice { get; set; }
        public ProductVariants ProductVariants { get; set; }
    }
}
