namespace Presentation.Contract.Attributes
{
    public class GetAllAttributesWithValuesResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<AttributeValueResponse> Values { get; set; } = new();
    }
}
