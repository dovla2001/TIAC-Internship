using FastEndpoints;
using MediatR;
using Presentation.Contract.Attributes;
using Presentation.Mappers;

namespace Presentation.Controllers.Attribute
{
    public class CreateAttribute : Endpoint<CreateAttributeRequest, CreateAttributeResponse>
    {
        private readonly IMediator _mediator;

        public CreateAttribute(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Post("attributes");
            Roles("Admin");
            //AllowAnonymous();
        }

        public override async Task HandleAsync(CreateAttributeRequest request, CancellationToken cancellationToken)
        {
            var attribute = await _mediator.Send(request.ToCommand(), cancellationToken);
            await SendOkAsync(attribute.ToApiResponseFromCommand(), cancellationToken);
        }
    }
}
