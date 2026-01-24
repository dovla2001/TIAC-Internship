using Application.AttributeValues.CommonAttributeValues;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.AttributesValues
{
    public class AttributesValuesRepository : IAttributeValuesRepository
    {
        private readonly TiacWebShopDbContext _dbContext;

        public AttributesValuesRepository(TiacWebShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Domain.Entities.AttributesValues> CreateAttributesValueAsync(Domain.Entities.AttributesValues attributesValue, CancellationToken cancellationToken)
        {
            var newAttributesValues = await _dbContext.AttributesValues.AddAsync(attributesValue, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return newAttributesValues.Entity;
        }

        public async Task DeleteAttributesValueAsync(Domain.Entities.AttributesValues attributesValue, CancellationToken cancellationToken)
        {
            _dbContext.AttributesValues.Remove(attributesValue);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Domain.Entities.AttributesValues?>> GetByIdAttributesValueAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.AttributesValues.Where(av => av.AttributeId == id).ToListAsync(cancellationToken);
        }

        public async Task UpdateAttrubutesValueAsync(Domain.Entities.AttributesValues attributesValue, CancellationToken cancellationToken)
        {
            _dbContext.AttributesValues.Update(attributesValue);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> DoesValueExistForAttributeAsync(int attributeId, string value, CancellationToken cancellationToken)
        {
            return await _dbContext.AttributesValues.AnyAsync(av => av.AttributeId == attributeId && av.Value.ToLower() == value.ToLower(), cancellationToken);
        }
    }
}
