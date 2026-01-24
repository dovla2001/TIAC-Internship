using Application.Product.CommonProducts;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Products
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly TiacWebShopDbContext _dbContext;

        public ProductsRepository(TiacWebShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Domain.Entities.Products> CreateProductsAsync(Domain.Entities.Products products, CancellationToken cancellationToken)
        {
            var newProducts = await _dbContext.Products.AddAsync(products, cancellationToken);
            await _dbContext.SaveChangesAsync();
            return newProducts.Entity;
        }

        public async Task DeleteProductAsync(Domain.Entities.Products product, CancellationToken cancellationToken)
        {
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Domain.Entities.Products>> GetAllProductsAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Products.ToListAsync(cancellationToken);
        }

        public async Task<Domain.Entities.Products?> GetbyIdProductAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == id, cancellationToken);
        }

        public async Task UpdateProductAsync(Domain.Entities.Products product, CancellationToken cancellationToken)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Domain.Entities.Products?> GetByIdProductVariantAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Products.Include(p => p.ProductVariants)
                .ThenInclude(pv => pv.VariantValues).ThenInclude(vv => vv.AttributeValue).ThenInclude(av => av.Attribute)
                .FirstOrDefaultAsync(p => p.ProductId == id, cancellationToken);
        }

        public async Task<(List<Domain.Entities.Products> Items, int TotalCount)> GetAllAsync(int pageNumber, int pageSize, string? sortBy, string? sortDirection, string? name, CancellationToken cancellationToken)
        {
            var query = _dbContext.Products.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(p => p.Name.ToLower().Contains(name.ToLower()));
            }

            var totalCount = await _dbContext.Products.CountAsync(cancellationToken); //neophodno da znamo koliko ima stavki u bazi

            bool isDescending = sortDirection?.ToLower() == "desc";

            IQueryable<Domain.Entities.Products> sortedQuery;

            if (sortBy?.ToLower() == "name")
            {
                sortedQuery = isDescending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name);
            }
            else if (sortBy?.ToLower() == "baseprice")
            {
                sortedQuery = isDescending ? query.OrderByDescending(p => p.BasePrice) : query.OrderBy(p => p.BasePrice);
            }
            else
            {
                sortedQuery = query.OrderBy(p => p.ProductId);
            }

            var items = await sortedQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            return (items, totalCount);
        }

        public async Task<List<Domain.Entities.Products>> GetAllSimpleAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Products.AsNoTracking().Select(p => new Domain.Entities.Products { ProductId = p.ProductId, Name = p.Name }).ToListAsync(cancellationToken);
        }
    }
}
