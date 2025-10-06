using Application.AttributeValues.Query;
using FastEndpoints;
using MediatR;
using Presentation.Contract.AttributeValues;
using Presentation.Mappers;

namespace Presentation.Controllers.AttributeValues
{
    public class GetAttributeValuesById : Endpoint<ReadAttributeValuesRequest, List<ReadAttributeValuesResponse>>
    {
        private IMediator _mediator;

        public GetAttributeValuesById(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("attributes/{attributeId}/values");
            Roles("Admin");
            //AllowAnonymous();
        }

        public override async Task HandleAsync(ReadAttributeValuesRequest request, CancellationToken cancellationToken)
        {
            var attributeValues = await _mediator.Send(new GetAttributeValuesByIdQuery(request.AttributeId), cancellationToken);

            var response = attributeValues.Select(v => new ReadAttributeValuesResponse
            {
                AttributeValuesId = v.AttributesValuesId,
                AttributeId = v.AttributeId,
                Value = v.Value
            }).ToList();

            await SendOkAsync(response, cancellationToken);
        }
    }
}
