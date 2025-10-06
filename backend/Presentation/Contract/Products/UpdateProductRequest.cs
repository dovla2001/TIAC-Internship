namespace Presentation.Contract.Products
{
    public class UpdateProductRequest
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public decimal BasePrice { get; set; }
        public string ImageUrl { get; set; }
    }
}
