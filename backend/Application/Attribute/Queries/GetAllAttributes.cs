using Application.Attribute.CommonAttributes;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Attribute.Queries
{
    public class GetAllAttributesQueryHandler : IRequestHandler<GetAllAttributesQuery, List<Attributes>>
    {
        private IAttributesRepository _attributesRepository;

        public GetAllAttributesQueryHandler(IAttributesRepository attributesRepository)
        {
            _attributesRepository = attributesRepository;
        }

        public async Task<List<Attributes>> Handle(GetAllAttributesQuery request, CancellationToken cancellationToken)
        {
            var attributes =  await _attributesRepository.GetAllAsync(cancellationToken);
            return attributes;
        }
    }

    public record GetAllAttributesQuery : IRequest<List<Attributes>>;
}
