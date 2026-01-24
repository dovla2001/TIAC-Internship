using Application.ProductVariant.CommonProductVariants;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ProductVariants
{
    public class ProductVariantsRepository : IProductsVariantsRepository
    {
        private readonly TiacWebShopDbContext _dbContex;

        public ProductVariantsRepository(TiacWebShopDbContext dbContex)
        {
            _dbContex = dbContex;
        }

        public async Task<Domain.Entities.ProductVariants> CreateProductVarianatsAsync(Domain.Entities.ProductVariants productVariants, CancellationToken cancellationToken)
        {
            var newProductVariant = await _dbContex.ProductVariants.AddAsync(productVariants, cancellationToken);
            await _dbContex.SaveChangesAsync(cancellationToken);
            return newProductVariant.Entity;
        }

        public async Task DeleteProductVariantsAsync(Domain.Entities.ProductVariants productVariants, CancellationToken cancellationToken)
        {
            _dbContex.ProductVariants.Remove(productVariants);
            await _dbContex.SaveChangesAsync(cancellationToken);
        }

        public async Task<Domain.Entities.ProductVariants?> GetByIdProductVariantsAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContex.ProductVariants.FirstOrDefaultAsync(pv => pv.ProductVariantsId == id, cancellationToken);
        }

        public async Task UpdateProductVariantsAsync(Domain.Entities.ProductVariants productVariants, CancellationToken cancellationToken)
        {
            _dbContex.ProductVariants.Update(productVariants);
            await _dbContex.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> DoesVariantExistAsync(int productId, List<int> attributeValuesIds, CancellationToken cancellationToken)
        {
            var sortedNewValuesIds = attributeValuesIds.OrderBy(id => id).ToList();

            var variantsForProduct = await _dbContex.ProductVariants
                .Include(pv => pv.VariantValues)
                .Where(pv => pv.ProductId == productId)
                .ToListAsync(cancellationToken);

            foreach (var variant in variantsForProduct)
            {
                var existingValuesIds = variant.VariantValues.Select(vv => vv.AttributeValueId).OrderBy(id => id).ToList();

                if (existingValuesIds.SequenceEqual(sortedNewValuesIds))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
