using Application.Attribute.Queries;
using FastEndpoints;
using MediatR;
using Presentation.Contract.Attributes;
using Presentation.Mappers;

namespace Presentation.Controllers.Attribute
{
    public class GetAllAttributesWithValues : EndpointWithoutRequest<List<GetAllAttributesWithValuesResponse>>
    {
        private IMediator _mediator;

        public GetAllAttributesWithValues(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("attributes/with-values");
            Roles("Admin");
            //AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken cancellationToken)
        {
            var attributesFromDb = await _mediator.Send(new GetAllAttributesWithValuesQuery(), cancellationToken);

            var response = attributesFromDb
                           .Select(attr => attr.ToAttributeWithValuesResponse())
                           .ToList();

            await SendOkAsync(response, cancellationToken);
        }
    }
}
