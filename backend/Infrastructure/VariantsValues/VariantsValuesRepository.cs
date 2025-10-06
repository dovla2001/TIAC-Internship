using Application.VariantsValues.CommonVariantValues;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.VariantsValues
{
    public class VariantsValuesRepository : IVariantValuesRepository
    {
        private readonly TiacWebShopDbContext _dbContext;

        public VariantsValuesRepository(TiacWebShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<VariantValues> CreateVariantsValueAsync(VariantValues variantValues, CancellationToken cancellationToken)
        {
            var newVariantsValue = await _dbContext.VariantValues.AddAsync(variantValues, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return newVariantsValue.Entity;
        }

        public async Task DeleteVariantValuesAsync(VariantValues variantValues, CancellationToken cancellationToken)
        {
            _dbContext.VariantValues.Remove(variantValues);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<VariantValues?> GetByIdVariantsValueAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.VariantValues.FirstOrDefaultAsync(vv => vv.VariantValuesId == id, cancellationToken);
        }

        public async Task UpdateVariantValuesAsync(VariantValues variantValues, CancellationToken cancellationToken)
        {
            _dbContext.VariantValues.Update(variantValues);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
