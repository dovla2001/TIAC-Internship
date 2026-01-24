using Domain.Entities;

namespace Application.ProductVariant.CommonProductVariants
{
    public interface IProductsVariantsRepository
    {
        public Task<ProductVariants?> GetByIdProductVariantsAsync(int id, CancellationToken cancellationToken);
        public Task<ProductVariants> CreateProductVarianatsAsync(ProductVariants productVariants, CancellationToken cancellationToken);
        public Task UpdateProductVariantsAsync(ProductVariants productVariants, CancellationToken cancellationToken);
        public Task DeleteProductVariantsAsync(ProductVariants productVariants, CancellationToken cancellationToken);
        public Task<bool> DoesVariantExistAsync(int productId, List<int> attributeValuesIds, CancellationToken cancellationToken);
    }
}
