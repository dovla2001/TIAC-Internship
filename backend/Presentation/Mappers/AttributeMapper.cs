using Application.Attribute.Command;
using Presentation.Contract.Attributes;
using Domain.Entities;

namespace Presentation.Mappers
{
    public static class AttributeMapper
    {
        public static CreateAttributesCommand ToCommand(this CreateAttributeRequest request) => new CreateAttributesCommand(request.Name);
    
        public static CreateAttributeResponse ToApiResponseFromCommand(this Attributes attributes)
        {
            return new CreateAttributeResponse
            {
                AttributeId = attributes.AttributesId,
                Name = attributes.Name,
            };
        }

        public static GetAllAttributesWithValuesResponse ToAttributeWithValuesResponse(this Attributes attributes)
        {
            return new GetAllAttributesWithValuesResponse
            {
                Id = attributes.AttributesId,
                Name = attributes.Name,
                Values = attributes.AttributeValues.Select(av => new AttributeValueResponse
                {
                    Id = av.AttributesValuesId,
                    Value = av.Value
                }).ToList()
            };
        }
    }
}
