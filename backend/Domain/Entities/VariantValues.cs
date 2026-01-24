namespace Domain.Entities
{
    public class VariantValues
    {
        public int VariantValuesId { get; set; }
        public int AttributeValueId { get; set; }
        public int ProductVariantId { get; set; }
        public AttributesValues AttributeValue { get; set; }
        public ProductVariants ProductVariant { get; set; }
    }
}
