namespace Presentation.Contract.Products
{
    public class ProductVariantDTO
    {
        public int VariantId { get; set; } 
        public decimal Price { get; set; }
        public List<AttributeDTO> Attributes { get; set; }  
    }
}
