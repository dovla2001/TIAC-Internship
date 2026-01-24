using Application.Attribute.CommonAttributes;
using MediatR;

namespace Application.Attribute.Command
{
    public class DeleteAttributesCommandHandler : IRequestHandler<DeleteAttributeCommand, bool>
    {
        private readonly IAttributesRepository _attributesRepository;

        public DeleteAttributesCommandHandler(IAttributesRepository attributesRepository)
        {
            _attributesRepository = attributesRepository;
        }

        public async Task<bool> Handle(DeleteAttributeCommand request, CancellationToken cancellationToken)
        {
            var existingAttribute = await _attributesRepository.GetByIdAttributesAsync(request.attributeId, cancellationToken);
            await _attributesRepository.DeleteAttributesAsync(existingAttribute, cancellationToken);

            return true;
        }
    }

    public record DeleteAttributeCommand(int attributeId) : IRequest<bool>;
}
