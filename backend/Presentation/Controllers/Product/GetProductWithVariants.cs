using Application.Product.Queries;
using FastEndpoints;
using MediatR;
using Presentation.Contract.Products;

namespace Presentation.Controllers.Product
{
    public class GetProductWithVariants : Endpoint<ReadProductRequest, ProductWithVariantsResponse>
    {
        private readonly IMediator _mediator;

        public GetProductWithVariants(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("products/{productId}/details");
            //AllowAnonymous();
            Roles("Admin", "Employee");
        }

        public override async Task HandleAsync(ReadProductRequest request, CancellationToken cancellationToken)
        {
            var query = new GetProductWithVariantsQuery(request.ProductId);
            var productFromDb = await _mediator.Send(query, cancellationToken);

            if (productFromDb is null)
            {
                await SendNotFoundAsync(cancellationToken);
            }

            var response = new ProductWithVariantsResponse
            {
                Id = productFromDb.ProductId,
                Name = productFromDb.Name,
                Description = productFromDb.Decsription,
                ImageUrl = productFromDb.ImageUrl,
                Variants = productFromDb.ProductVariants.Select(variant => new ProductVariantDTO
                {
                    VariantId = variant.ProductVariantsId,
                    Price = (decimal)variant.Price, //ispraviti decimal?
                    Attributes = variant.VariantValues.Select(variantValue => new AttributeDTO
                    {
                        AttributeName = variantValue.AttributeValue.Attribute.Name,
                        AttributeValue = variantValue.AttributeValue.Value
                    }).ToList()
                }).ToList()
            };

            await SendOkAsync(response, cancellationToken);
        }
    }
}
