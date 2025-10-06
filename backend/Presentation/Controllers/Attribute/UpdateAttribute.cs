using Application.Attribute.Command;
using FastEndpoints;
using MediatR;
using Presentation.Contract.Attributes;

namespace Presentation.Controllers.Attribute
{
    public class UpdateAttribute : Endpoint<UpdateAttributeRequest, UpdateAttributeResponse>
    {
        public IMediator _mediator;

        public UpdateAttribute(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Put("attributes/{attributeId}");
            Roles("Admin");
            //AllowAnonymous();
        }

        public override async Task HandleAsync(UpdateAttributeRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateAttributesCommand(request.AttributeId, request.Name);
            var updatedAttribute = await _mediator.Send(command, cancellationToken);

            var response = new UpdateAttributeResponse
            {
                AttributeId = request.AttributeId,
                Name = updatedAttribute.Name
            };

            await SendOkAsync(response, cancellationToken);
        }
    }
}
