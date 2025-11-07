using Grbus.WebShop.Application.Baskets.DTOs;
using Grbus.WebShop.Application.Common;
using Grbus.WebShop.Domain.Aggregates.Baskets.Repository;
using Grbus.WebShop.Domain.Aggregates.Products;
using Grbus.WebShop.Domain.Common;
using MediatR;

namespace Grbus.WebShop.Application.Baskets.Queries
{
    public record GetBasketHistoryWithPaginationQuery : IRequest<Result<PaginatedList<BasketHistoryDto>>>
    {
        public required string Email { get; init; }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetBasketHistoryWithPaginationQueryHandler : IRequestHandler<GetBasketHistoryWithPaginationQuery, Result<PaginatedList<BasketHistoryDto>>>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IProductRepository _productRepository;

        public GetBasketHistoryWithPaginationQueryHandler(
            IBasketRepository basketRepository,
            IProductRepository productRepository
            )
        {
            _basketRepository = basketRepository;
            _productRepository = productRepository;
        }

        public async Task<Result<PaginatedList<BasketHistoryDto>>> Handle(GetBasketHistoryWithPaginationQuery request, CancellationToken ct)
        {
            var query = from h in _basketRepository.GetHistory(request.Email)
                        join p in _productRepository.GetQueriable() on h.ProductId equals p.Id
                        select new BasketHistoryDto
                        {
                            Id = h.Id,
                            Timestamp = h.Timestamp,
                            ProductId = h.ProductId,
                            ProductName = p.Name,
                            Quantity = h.Quantity,
                        };
            
            
            var result = Result<PaginatedList<BasketHistoryDto>>.Success(query.ToPaginatedList(request.PageNumber, request.PageSize));

            return await Task.FromResult(result);
        }
    }
}
