using Application.Product.Command;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
