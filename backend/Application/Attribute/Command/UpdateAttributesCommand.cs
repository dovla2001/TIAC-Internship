using Application.Attribute.CommonAttributes;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Attribute.Command
{
    public class UpdateAttributesCommandHandler : IRequestHandler<UpdateAttributesCommand, Attributes>
    {
        private readonly IAttributesRepository _attributesRepository;

        public UpdateAttributesCommandHandler(IAttributesRepository attributesRepository)
        {
            _attributesRepository = attributesRepository;
        }

        public async Task<Attributes> Handle(UpdateAttributesCommand request, CancellationToken cancellationToken)
        {
            var existingAttribute = await _attributesRepository.GetByIdAttributesAsync(request.AttributeId, cancellationToken);
            if (existingAttribute is null)
            {
                throw new Exception("This attribute doesn't exist!");
            }

            existingAttribute.Name = request.Name;

            await _attributesRepository.UpdateAttrubutesAsync(existingAttribute, cancellationToken);

            return existingAttribute;
        }
    }

    public record UpdateAttributesCommand(int AttributeId, string Name) : IRequest<Attributes>;
}
