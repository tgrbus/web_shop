using Grbus.WebShop.Application.Common;
using Grbus.WebShop.Application.Products.DTOs;
using Grbus.WebShop.Domain.Aggregates.Products;
using Grbus.WebShop.Domain.Common;
using MediatR;

namespace Grbus.WebShop.Application.Products.Queries
{
    public record GetProductByIdQuery : IRequest<Result<ProductDto>>
    {
        public int Id { get; set; }
    }

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductDto>> 
    {
        private readonly ICachingService _cachingService;
        private readonly IProductRepository _productRepo;

        public GetProductByIdQueryHandler(ICachingService cachingService, IProductRepository productRepo)
        {
            _cachingService = cachingService;
            _productRepo = productRepo;
        }

        public async Task<Result<ProductDto>> Handle(GetProductByIdQuery query, CancellationToken ct)
        {
            if(_cachingService.TryGet<ProductDto>($"product_{query.Id}", out var cachedProduct))
            {
                return Result<ProductDto>.Success(cachedProduct!);                
            }

            var product = await _productRepo.GetByIdAsync(query.Id);

            if (product == null)
            {
                return Result<ProductDto>.Failure(ErrorLists.RecordDoesNotExistForGivenKey);
            }

            cachedProduct = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                SKU = product.SKU,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                TaxPercentage = product.TaxPercentage,
            };
            await _cachingService.Set($"product_{query.Id}", cachedProduct);
            
            return Result<ProductDto>.Success(cachedProduct);
        }
    }
}
