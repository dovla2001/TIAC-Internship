using Application.Product.CommonProducts;
using Application.Product.Mappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Product.Command
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Products>
    {
        private readonly IProductsRepository _productsRepository;

        public CreateProductCommandHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<Products> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var domainProduct = request.ToDomainEntity();
            var persistedProduct = await _productsRepository.CreateProductsAsync(domainProduct, cancellationToken);
            return persistedProduct;
        }
    }
}
