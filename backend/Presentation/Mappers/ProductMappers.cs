using Domain.Entities;
using Presentation.Contract.Products;

namespace Presentation.Mappers
{
    public static class ProductMappers
    {

        public static ReadProductResponse ToApiResponse(this Products products)
        {
            return new ReadProductResponse
            {
                ProductId = products.ProductId,
                Name = products.Name,
                Description = products.Decsription,
                BasePrice = products.BasePrice,
                ImageUrl = products.ImageUrl
            };
        }

        public static CreateProductResponse ToApiResponseFromCommand(this Products products)
        {
            return new CreateProductResponse
            {
                ProductId = products.ProductId,
                Name = products.Name,
                Description = products.Decsription,
                BasePrice = products.BasePrice,
                ImageUrl = products.ImageUrl
            };
        }

        public static ReadProductForAdmin ToSimpleApiResponse(this Products products)
        {
            return new ReadProductForAdmin
            {
                Id = products.ProductId,
                Name = products.Name
            };
        }
    }
}
