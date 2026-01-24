namespace Presentation.Contract.ProductVariant
{
    public class CreateProductVariantRequest
    {
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public List<int> AttributeValueIds { get; set; }
    }
}
