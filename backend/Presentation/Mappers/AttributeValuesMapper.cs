using Domain.Entities;
using Presentation.Contract.AttributeValues;

namespace Presentation.Mappers
{
    public static class AttributeValuesMapper
    {

        public static ReadAttributeValuesResponse ToApiResponse(this AttributesValues attributesValues)
        {
            return new ReadAttributeValuesResponse
            {
                AttributeValuesId = attributesValues.AttributesValuesId,
                AttributeId = attributesValues.AttributeId,
                Value = attributesValues.Value
            };
        }

        public static CreateAttributeValuesResponse ToApiResponseFromCommand(this AttributesValues attributesValues)
        {
            return new CreateAttributeValuesResponse
            {
                AttributeValueId = attributesValues.AttributesValuesId,
                AttributeId = attributesValues.AttributeId,
                Value = attributesValues.Value
            };
        }
    }
}
