using Application.Product.CommonProducts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Product.Command
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductsRepository _productRepository;

        public DeleteProductCommandHandler(IProductsRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var existingProduct = await _productRepository.GetbyIdProductAsync(request.productId, cancellationToken);
            if (existingProduct is null)
            {
                throw new Exception("This product doesn't exist!");
                //return false;
            }

            await _productRepository.DeleteProductAsync(existingProduct, cancellationToken);

            return true;
        }
    }
}
