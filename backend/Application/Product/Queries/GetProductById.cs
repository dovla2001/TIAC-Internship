using Application.Product.CommonProducts;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Product.Queries
{
    public class GetProductById
    {
        public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Products>
        {
            private readonly IProductsRepository _productRepository;

            public GetProductByIdHandler(IProductsRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task<Products> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetbyIdProductAsync(request.Id, cancellationToken);
                if (product is null)
                {
                    throw new Exception($"Product with id {request.Id} doesn't exists!");
                }

                return product;
            }
        }

        public record GetProductByIdQuery(int Id) : IRequest<Products>;
    }
}
