using Domain.Entities;

namespace Application.AttributeValues.CommonAttributeValues
{
    public interface IAttributeValuesRepository
    {
        public Task<List<AttributesValues?>> GetByIdAttributesValueAsync(int id, CancellationToken cancellationToken);

        public Task<AttributesValues> CreateAttributesValueAsync(AttributesValues attributesValue, CancellationToken cancellationToken);
        public Task UpdateAttrubutesValueAsync(AttributesValues attributesValue, CancellationToken cancellationToken);
        public Task DeleteAttributesValueAsync(AttributesValues attributesValue, CancellationToken cancellationToken);
        public Task<bool> DoesValueExistForAttributeAsync(int attributeId, string value, CancellationToken cancellationToken);
    }
}
