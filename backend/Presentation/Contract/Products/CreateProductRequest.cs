namespace Presentation.Contract.Products
{
    public class CreateProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public IFormFile Image { get; set; }
    }
}
