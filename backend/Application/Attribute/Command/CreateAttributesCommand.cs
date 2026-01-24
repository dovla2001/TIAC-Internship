using Application.Attribute.CommonAttributes;
using Application.Exceptions;
using Domain.Entities;
using MediatR;

namespace Application.Attribute.Command
{
    public class CreateAttributesCommandHandler : IRequestHandler<CreateAttributesCommand, Attributes>
    {
        private readonly IAttributesRepository _attributesRepository;

        public CreateAttributesCommandHandler(IAttributesRepository attributesRepository)
        {
            _attributesRepository = attributesRepository;
        }

        public async Task<Attributes> Handle(CreateAttributesCommand request, CancellationToken cancellationToken)
        {
            var attributeExist = await _attributesRepository.DoesAttributeExistByNameAsync(request.Name, cancellationToken);

            if (attributeExist)
            {
                throw new DuplicateAttributeException("This attribute already exist!");
            }

            var newAttribute = new Attributes { Name = request.Name };
            return await _attributesRepository.CreateAttributesAsync(newAttribute, cancellationToken);
        }
    }

    public record CreateAttributesCommand(string Name) : IRequest<Attributes>;
}
