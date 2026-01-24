using Application.AttributeValues.Command;
using FastEndpoints;
using MediatR;
using Presentation.Contract.AttributeValues;
using Presentation.Mappers;

namespace Presentation.Controllers.AttributeValues
{
    public class CreateAttributeValues : Endpoint<CreateAttributeValuesRequest, CreateAttributeValuesResponse>
    {
        private readonly IMediator _mediator;

        public CreateAttributeValues(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Post("attributes/{attributeId}/values");
            Roles("Admin");
            //AllowAnonymous();
        }

        public override async Task HandleAsync(CreateAttributeValuesRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateAttributeValuesCommand(request.AttributeId, request.Value);

            var result = await _mediator.Send(command, cancellationToken);

            await SendOkAsync(result.ToApiResponseFromCommand(), cancellationToken);
        }
    }
}
