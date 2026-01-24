namespace Domain.Entities
{
    public class AttributesValues
    {
        public int AttributesValuesId { get; set; }
        public string Value { get; set; } = null!;

        public int AttributeId { get; set; }
        public Attributes Attribute { get; set; }

        public ICollection<VariantValues> VariantValues { get; set; } = new List<VariantValues>();
    }
}
