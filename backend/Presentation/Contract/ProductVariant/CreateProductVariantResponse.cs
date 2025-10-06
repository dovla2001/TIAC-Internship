namespace Presentation.Contract.ProductVariant
{
    public class CreateProductVariantResponse
    {
        public CreateProductVariantResponse()
        {
        }

        public int ProductVariantId { get; set; }
        public int ProductId { get; set; }
        public decimal? Price { get; set; }
        public List<int> AttributeValueIds { get; set; }

        public CreateProductVariantResponse(int productVariantId, int productId, decimal? price, List<int> attributeValueIds)
        {
            ProductVariantId = productVariantId;
            ProductId = productId;
            Price = price;
            AttributeValueIds = attributeValueIds;
        }
    }
}
