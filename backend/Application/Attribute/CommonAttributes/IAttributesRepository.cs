using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Application.Attribute.CommonAttributes
{
    public interface IAttributesRepository
    {
        public Task<Attributes?> GetByIdAttributesAsync(int id, CancellationToken cancellationToken);
        public Task<Attributes> CreateAttributesAsync(Attributes attributes, CancellationToken cancellationToken);
        public Task UpdateAttrubutesAsync(Attributes attributes, CancellationToken cancellationToken);
        public Task DeleteAttributesAsync(Attributes attributes, CancellationToken cancellationToken);
        public Task<List<Attributes>> GetAllAsync(CancellationToken cancellationToken);

        public Task<List<Attributes>> GetAttributesWithValuesAsync(CancellationToken cancellationToken);
        public Task<bool> DoesAttributeExistByNameAsync(string name, CancellationToken cancellationToken);
    }
}
