using Application.Attribute.CommonAttributes;
using Domain.Entities;
using MediatR;

namespace Application.Attribute.Queries
{
    public class GetAllAttributesWithValuesHandler : IRequestHandler<GetAllAttributesWithValuesQuery, List<Attributes>>
    {
        private readonly IAttributesRepository _attributesRepository;

        public GetAllAttributesWithValuesHandler(IAttributesRepository attributesRepository)
        {
            _attributesRepository = attributesRepository;
        }

        public async Task<List<Attributes>> Handle(GetAllAttributesWithValuesQuery request, CancellationToken cancellationToken)
        {
            return await _attributesRepository.GetAttributesWithValuesAsync(cancellationToken);
        }
    }

    public record GetAllAttributesWithValuesQuery() : IRequest<List<Attributes>>;
}
