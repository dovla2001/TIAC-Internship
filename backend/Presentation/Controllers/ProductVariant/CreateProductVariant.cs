using Application.ProductVariant.Command;
using FastEndpoints;
using MediatR;
using Presentation.Contract.ProductVariant;

namespace Presentation.Controllers.ProductVariant
{
    public class CreateProductVariant : Endpoint<CreateProductVariantRequest, CreateProductVariantResponse>
    {
        private readonly IMediator _mediator;

        public CreateProductVariant(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Post("productVariant");
            Roles("Admin");
            //AllowAnonymous();
        }

        public override async Task HandleAsync(CreateProductVariantRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateProductVariantCommand(request.ProductId, request.Price, request.AttributeValueIds);

            var createdVariant = await _mediator.Send(command, cancellationToken);

            var response = new CreateProductVariantResponse(createdVariant.ProductVariantsId, createdVariant.ProductId, createdVariant.Price, createdVariant.VariantValues.Select(vv => vv.AttributeValueId).ToList());

            await SendOkAsync(response, cancellationToken);
        }
    }
}
