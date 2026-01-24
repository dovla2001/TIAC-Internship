using Application.Product.CommonProducts;
using Domain.Entities;
using MediatR;

namespace Application.Product.Queries
{
    public class GetAllProducts
    {
        public class GetAllProductHandler : IRequestHandler<GetAllProductsQuery, List<Products>>
        {
            private readonly IProductsRepository _productsRepository;

            public GetAllProductHandler(IProductsRepository productsRepository)
            {
                _productsRepository = productsRepository;
            }

            public async Task<List<Products>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
            {
                var products = await _productsRepository.GetAllSimpleAsync(cancellationToken);

                return products;
            }
        }

        public record GetAllProductsQuery() : IRequest<List<Products>>;
    }
}
