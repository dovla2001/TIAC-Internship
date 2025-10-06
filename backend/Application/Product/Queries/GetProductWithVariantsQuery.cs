using Application.Product.CommonProducts;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Product.Queries
{
    public class GetProductWithVariantsQueryHandler : IRequestHandler<GetProductWithVariantsQuery, Products>
    {
        private readonly IProductsRepository _productsRepository;

        public GetProductWithVariantsQueryHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<Products> Handle(GetProductWithVariantsQuery request, CancellationToken cancellationToken)
        {
            var product = await _productsRepository.GetByIdProductVariantAsync(request.ProductId, cancellationToken);
            if (product == null)
            {
                return null;
            }

            return product;
        }
    }

    public record GetProductWithVariantsQuery(int ProductId) : IRequest<Products>;
}
