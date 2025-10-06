using Application.AttributeValues.CommonAttributeValues;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AttributeValues.Query
{
    public class GetAttributeValuesByIdHandler : IRequestHandler<GetAttributeValuesByIdQuery, List<AttributesValues>>
    {
        private readonly IAttributeValuesRepository _attributeValuesRepository;

        public GetAttributeValuesByIdHandler(IAttributeValuesRepository attributeValuesRepository)
        {
            _attributeValuesRepository = attributeValuesRepository;
        }

        public async Task<List<AttributesValues>> Handle(GetAttributeValuesByIdQuery request, CancellationToken cancellationToken)
        {
            var attributeValues = await _attributeValuesRepository.GetByIdAttributesValueAsync(request.attributeId, cancellationToken);
            if (attributeValues is null)
            {
                throw new Exception($"AttributeValues with id {request.attributeId} doesn't exist!");
            }

            return attributeValues;
        }
    }

    public record GetAttributeValuesByIdQuery(int attributeId) : IRequest<List<AttributesValues>>;
}
