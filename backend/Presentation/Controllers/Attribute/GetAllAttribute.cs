using Application.Attribute.Queries;
using FastEndpoints;
using MediatR;
using Presentation.Contract.Attributes;

namespace Presentation.Controllers.Attribute
{
    public class GetAllAttribute : EndpointWithoutRequest<List<ReadAttributeResponse>>
    {
        private IMediator _mediator;

        public GetAllAttribute(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("attributes/getAll");
            Roles("Admin", "Employee");
            //AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken cancellationToken)
        {
            var query = new GetAllAttributesQuery();
            var result = await _mediator.Send(query, cancellationToken);

            var response = result.Select(a => new ReadAttributeResponse
            {
                AttributeId = a.AttributesId,
                Name = a.Name
            }).ToList();

            await SendOkAsync(response, cancellationToken);
        }
    }
}
