using Application.Attribute.Command;
using FastEndpoints;
using MediatR;
using Presentation.Contract.Attributes;

namespace Presentation.Controllers.Attribute
{
    public class DeleteAttribute : EndpointWithoutRequest<DeleteAttributeResponse>
    {
        private IMediator _mediator;

        public DeleteAttribute(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Delete("attributes/{attributeId}");
            Roles("Admin");
            //AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken cancellationToken)
        {
            var attributeId = Route<int>("attributeId");
            var deletedAttribute = await _mediator.Send(new DeleteAttributeCommand(attributeId), cancellationToken);

            if (!deletedAttribute)
            {
                await SendNotFoundAsync(cancellationToken);
            }

            await SendNoContentAsync(cancellationToken);
        }
    }
}
