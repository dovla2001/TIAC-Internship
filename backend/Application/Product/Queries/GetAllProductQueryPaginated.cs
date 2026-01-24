using Application.Product.CommonProducts;
using Domain.Entities;
using MediatR;

namespace Application.Product.Queries
{
    public class GetAllProductQueryPaginated
    {
        public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, (List<Products> Items, int totalCount)>
        {
            private readonly IProductsRepository _productsRepository;

            public GetAllProductQueryHandler(IProductsRepository productsRepository)
            {
                _productsRepository = productsRepository;
            }

            public async Task<(List<Products> Items, int totalCount)> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
            {
                var (products, totalCount) = await _productsRepository.GetAllAsync(request.PageNumber, request.PageSize, request.SortBy, request.SortDirection, request.Name, cancellationToken);

                return (products, totalCount);
            }
        }

        public record GetAllProductQuery(int PageNumber, int PageSize, string? SortBy, string? SortDirection, string? Name) : IRequest<(List<Products> Items, int TotalCount)>;
    }
}
