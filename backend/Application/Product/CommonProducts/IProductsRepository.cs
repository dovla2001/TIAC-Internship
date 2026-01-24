using Domain.Entities;

namespace Application.Product.CommonProducts
{
    public interface IProductsRepository
    {
        public Task<Products> CreateProductsAsync(Products products, CancellationToken cancellationToken);
        public Task<Products?> GetbyIdProductAsync(int id, CancellationToken cancellationToken);
        public Task UpdateProductAsync(Products product, CancellationToken cancellationToken);
        public Task DeleteProductAsync(Products product, CancellationToken cancellationToken);

        public Task<List<Products>> GetAllProductsAsync(CancellationToken cancellationToken);
        public Task<Products?> GetByIdProductVariantAsync(int id, CancellationToken cancellationToken);
        public Task<(List<Products> Items, int TotalCount)> GetAllAsync(int pageNumber, int pageSize, string? sortBy, string? sortDirection, string? name, CancellationToken cancellationToken);
        public Task<List<Products>> GetAllSimpleAsync(CancellationToken cancellationToken);

    }
}
