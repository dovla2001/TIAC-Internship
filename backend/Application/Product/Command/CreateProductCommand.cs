using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Product.Command
{
    public record CreateProductCommand(string Name, string Description, decimal BasePrice, string ImageUrl) : IRequest<Products>;
    public record DeleteProductCommand(int productId) : IRequest<bool>;
    public record UpdateProductCommand(int productId, string Name, string Description, decimal BasePrice, string ImageUrl) : IRequest<Products>;
}
