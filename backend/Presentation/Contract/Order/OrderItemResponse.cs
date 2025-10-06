namespace Presentation.Contract.Order
{
    public class OrderItemResponse
    {
        public string FullProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal PricePerItem { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
