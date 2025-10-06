using Application.Exceptions;
using Application.ProductVariant.CommonProductVariants;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProductVariant.Command
{
    public class CreateProductVariantCommandHandler : IRequestHandler<CreateProductVariantCommand, ProductVariants>
    {
        private readonly IProductsVariantsRepository _productsVariantsRepository;

        public CreateProductVariantCommandHandler(IProductsVariantsRepository productsVariantsRepository)
        {
            _productsVariantsRepository = productsVariantsRepository;
        }

        public async Task<ProductVariants> Handle(CreateProductVariantCommand request, CancellationToken cancellationToken)
        {
            var variantExists = await _productsVariantsRepository.DoesVariantExistAsync(request.ProductId, request.AttributeValuesIds, cancellationToken);

            if (variantExists)
            {
                throw new DuplicateProductVariantException("Product with these attributes already exist!");
            }

            var newVariant = new ProductVariants
            {
                ProductId = request.ProductId,
                Price = request.Price
            };

            foreach (var valueId in request.AttributeValuesIds)
            {
                newVariant.VariantValues.Add(new VariantValues
                {
                    AttributeValueId = valueId,
                });
            }

            return await _productsVariantsRepository.CreateProductVarianatsAsync(newVariant, cancellationToken);
        }
    }

    public record CreateProductVariantCommand(int ProductId, decimal Price, List<int> AttributeValuesIds) : IRequest<ProductVariants>;
}
