using Domain.Entities;

namespace Application.VariantsValues.CommonVariantValues
{
    public interface IVariantValuesRepository
    {
        public Task<VariantValues?> GetByIdVariantsValueAsync(int id, CancellationToken cancellationToken);
        public Task<VariantValues> CreateVariantsValueAsync(VariantValues variantValues, CancellationToken cancellationToken);
        public Task UpdateVariantValuesAsync(VariantValues variantValues, CancellationToken cancellationToken);
        public Task DeleteVariantValuesAsync(VariantValues variantValues, CancellationToken cancellationToken);
    }
}
