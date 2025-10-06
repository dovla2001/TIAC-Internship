namespace Presentation.Contract.Order
{
    public class OrderListResponse
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int NumberOfItems { get; set; }
    }
}
