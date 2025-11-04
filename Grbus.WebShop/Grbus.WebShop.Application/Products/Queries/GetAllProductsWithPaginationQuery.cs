using Grbus.WebShop.Application.Common;
using Grbus.WebShop.Application.Products.DTOs;
using Grbus.WebShop.Domain.Aggregates.Products;
using Grbus.WebShop.Domain.Common;
using MediatR;

namespace Grbus.WebShop.Application.Products.Queries
{
    public record GetAllProductsWithPaginationQuery : IRequest<Result<PaginatedList<ProductDto>>>
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetAllProductsWithPaginationQueryHandler : IRequestHandler<GetAllProductsWithPaginationQuery, Result<PaginatedList<ProductDto>>>
    {
        private readonly IProductRepository _productRepo;
        public GetAllProductsWithPaginationQueryHandler(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }
        
        public async Task<Result<PaginatedList<ProductDto>>> Handle(GetAllProductsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var result = _productRepo.GetQueriable().Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                SKU = p.SKU,
                Price = p.Price,   
                TaxPercentage = p.TaxPercentage,
                StockQuantity = p.StockQuantity,
            });

            return await Task.FromResult(Result<PaginatedList<ProductDto>>.Success(result.ToPaginatedList(request.PageNumber, request.PageSize)));
        }
    }
}
