using Application.Attribute.CommonAttributes;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Attributes
{
    public class AttributesRepository : IAttributesRepository
    {

        private readonly TiacWebShopDbContext _dbcontext;

        public AttributesRepository(TiacWebShopDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Domain.Entities.Attributes> CreateAttributesAsync(Domain.Entities.Attributes attributes, CancellationToken cancellationToken)
        {
            var newAttributes = await _dbcontext.Attributes.AddAsync(attributes, cancellationToken);
            await _dbcontext.SaveChangesAsync(cancellationToken);
            return newAttributes.Entity;
        }

        public async Task DeleteAttributesAsync(Domain.Entities.Attributes attributes, CancellationToken cancellationToken)
        {
            _dbcontext.Attributes.Remove(attributes);
            await _dbcontext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Domain.Entities.Attributes>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbcontext.Attributes.ToListAsync(cancellationToken);
        }

        public async Task<Domain.Entities.Attributes?> GetByIdAttributesAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbcontext.Attributes.FirstOrDefaultAsync(a => a.AttributesId == id, cancellationToken);
        }

        public async Task UpdateAttrubutesAsync(Domain.Entities.Attributes attributes, CancellationToken cancellationToken)
        {
            _dbcontext.Attributes.Update(attributes);
            await _dbcontext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Domain.Entities.Attributes>> GetAttributesWithValuesAsync(CancellationToken cancellationToken)
        {
            return await _dbcontext.Attributes.Include(a => a.AttributeValues).AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<bool> DoesAttributeExistByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _dbcontext.Attributes.AnyAsync(a => a.Name.ToLower() ==  name.ToLower(), cancellationToken);
        }
    }
}
