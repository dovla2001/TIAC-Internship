using Application.AttributeValues.CommonAttributeValues;
using Application.Exceptions;
using Domain.Entities;
using MediatR;

namespace Application.AttributeValues.Command
{
    public class CreateAttributeValuesCommandHandler : IRequestHandler<CreateAttributeValuesCommand, AttributesValues>
    {
        private readonly IAttributeValuesRepository _attributeValuesRepository;

        public CreateAttributeValuesCommandHandler(IAttributeValuesRepository attributeValuesRepository)
        {
            _attributeValuesRepository = attributeValuesRepository;
        }

        public async Task<AttributesValues> Handle(CreateAttributeValuesCommand request, CancellationToken cancellationToken)
        {
            var valueExist = await _attributeValuesRepository.DoesValueExistForAttributeAsync(request.AttributeId, request.Value, cancellationToken);

            if (valueExist)
            {
                throw new DuplicateAttributeValueException("This value already exist for this attribute!");
            }

            var newAttributeValue = new AttributesValues
            {
                AttributeId = request.AttributeId,
                Value = request.Value
            };

            return await _attributeValuesRepository.CreateAttributesValueAsync(newAttributeValue, cancellationToken);
        }
    }

    public record CreateAttributeValuesCommand(int AttributeId, string Value) : IRequest<AttributesValues>;
}
