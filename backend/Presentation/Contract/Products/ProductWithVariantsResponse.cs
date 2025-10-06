namespace Presentation.Contract.Products
{
    public class ProductWithVariantsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<ProductVariantDTO> Variants { get; set; }
    }
}
