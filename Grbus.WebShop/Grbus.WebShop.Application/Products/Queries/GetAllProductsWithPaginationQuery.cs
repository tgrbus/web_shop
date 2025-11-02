using Grbus.WebShop.Application.Common;
using Grbus.WebShop.Application.Products.DTOs;
using Grbus.WebShop.Domain.Aggregates.Products;
using MediatR;

namespace Grbus.WebShop.Application.Products.Queries
{
    public record GetAllProductsWithPaginationQuery : IRequest<PaginatedList<ProductDto>>
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetAllProductsWithPaginationQueryHandler : IRequestHandler<GetAllProductsWithPaginationQuery, PaginatedList<ProductDto>>
    {
        private readonly IProductRepository _productRepo;
        public GetAllProductsWithPaginationQueryHandler(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }
        
        public async Task<PaginatedList<ProductDto>> Handle(GetAllProductsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var result = _productRepo.GetQueriable().Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,                
            });

            return await Task.FromResult(result.ToPaginatedList(request.PageNumber, request.PageSize));
        }
    }
}
