using FastEndpoints;
using MediatR;
using Presentation.Common;
using Presentation.Contract.Products;
using Presentation.Mappers;
using static Application.Product.Queries.GetAllProductQueryPaginated;
using static Application.Product.Queries.GetAllProducts;

namespace Presentation.Controllers.Product
{
    public class GetAllProducts: Endpoint<GetAllProductRequest, PagedList<ReadProductResponse>>
    {
        private readonly IMediator _mediator;

        public GetAllProducts(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("products/getAll");
            //AllowAnonymous();
            Roles("Employee");
        }

        public override async Task HandleAsync(GetAllProductRequest request, CancellationToken cancellationToken)
        {
            var pageNumber = request.PageNumber ?? 1;
            var pageSize = request.PageSize ?? 5;

            var query = new GetAllProductQuery(pageNumber, pageSize, request.SortBy, request.SortDirection, request.Name);

            var (productEntities, totalCount) = await _mediator.Send(query, cancellationToken);

            var response = productEntities.Select(p => new ReadProductResponse
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Decsription,
                BasePrice = p.BasePrice,
                ImageUrl = p.ImageUrl
            }).ToList();

            var pagedResponse = new PagedList<ReadProductResponse>(
                response,
                pageNumber,
                pageSize,
                totalCount);

            await SendOkAsync(pagedResponse, cancellationToken);    
        }
    }
}
