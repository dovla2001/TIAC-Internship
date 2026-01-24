using Application.Product.Command;
using FastEndpoints;
using MediatR;
using Presentation.Contract.Products;
using Presentation.Mappers;
using Presentation.Validators.Product;

namespace Presentation.Controllers.Product
{
    public class CreateProduct : Endpoint<CreateProductRequest, CreateProductResponse>
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreateProduct(IMediator mediator, IWebHostEnvironment webHostEnvironment)
        {
            _mediator = mediator;
            _webHostEnvironment = webHostEnvironment;
        }

        public override void Configure()
        {
            Post("products");
            //AllowAnonymous();
            Roles("Admin");
            AllowFileUploads();
            Validator<CreateProductRequestValidation>();
        }

        public override async Task HandleAsync(CreateProductRequest request, CancellationToken cancellationToken)
        {
            string imageUrl = null;
            if (request.Image != null)
            {
                var uniqueFileName = $"{request.Image.FileName}";

                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                Directory.CreateDirectory(uploadsFolder);

                await using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await request.Image.CopyToAsync(fileStream, cancellationToken);
                }

                imageUrl = $"/images//{uniqueFileName}";
            }

            var command = new CreateProductCommand(
                request.Name,
                request.Description,
                request.BasePrice,
                imageUrl
            );

            var products = await _mediator.Send(command, cancellationToken);
            await SendOkAsync(products.ToApiResponseFromCommand(), cancellationToken);
        }
    }
}
