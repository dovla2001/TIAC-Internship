using Application.Product.Command;
using Domain.Entities;

namespace Application.Product.Mappers
{
    public static class ProductMapper
    {
        public static Products ToDomainEntity(this CreateProductCommand command)
        {
            return new Products
            {
                Name = command.Name,
                Decsription = command.Description,
                BasePrice = command.BasePrice,
                ImageUrl = command.ImageUrl
            };
        }
    }
}
