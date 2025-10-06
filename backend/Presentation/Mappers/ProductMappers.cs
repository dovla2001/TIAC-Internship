using Application.Employee.Command;
using Application.Product.Command;
using Domain.Entities;
using Presentation.Contract.Employees;
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

        //public static CreateProductCommand ToCommand(this CreateProductRequest request) => new CreateProductCommand(request.Name, request.Description, request.BasePrice, request.Image);

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
