using Application.Product.CommonProducts;
using Domain.Entities;
using MediatR;

namespace Application.Product.Command
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Products>
    {
        private readonly IProductsRepository _productRepository;

        public UpdateProductCommandHandler(IProductsRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Products> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var existingProduct = await _productRepository.GetbyIdProductAsync(request.productId, cancellationToken);
            if (existingProduct is null)
            {
                throw new Exception("This product doesn't exist!");
            }

            existingProduct.Name = request.Name;
            existingProduct.Decsription = request.Description;
            existingProduct.BasePrice = request.BasePrice;
            existingProduct.ImageUrl = request.ImageUrl;

            await _productRepository.UpdateProductAsync(existingProduct, cancellationToken);

            return existingProduct;
        }
    }
}
