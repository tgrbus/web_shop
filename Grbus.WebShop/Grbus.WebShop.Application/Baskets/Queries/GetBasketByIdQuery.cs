using Grbus.WebShop.Application.Baskets.DTOs;
using Grbus.WebShop.Application.Common;
using Grbus.WebShop.Application.Products.DTOs;
using Grbus.WebShop.Domain.Aggregates.Baskets;
using Grbus.WebShop.Domain.Aggregates.Baskets.Repository;
using Grbus.WebShop.Domain.Aggregates.Products;
using Grbus.WebShop.Domain.Common;
using MediatR;

namespace Grbus.WebShop.Application.Baskets.Queries
{
    public record GetBasketByIdQuery : IRequest<Result<BasketDto>>
    {
        public required string CustomerEmail { get; init; }
    }

    public class GetBasketByIdQueryHandler : IRequestHandler<GetBasketByIdQuery, Result<BasketDto>>
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IProductRepository _productRepo;

        public GetBasketByIdQueryHandler(IBasketRepository basketRepo, IProductRepository productRepo)
        {
            _basketRepo = basketRepo;
            _productRepo = productRepo;
        }

        public async Task<Result<BasketDto>> Handle(GetBasketByIdQuery request, CancellationToken cancellationToken)
        {
            var basket = await _basketRepo.GetBasketByIdAsync(request.CustomerEmail);

            if(basket == null)
            {
                return Result<BasketDto>.Failure(ApplicationErrors.BasketNotFound);
            }            

            var products = _productRepo.GetQueriable().Where(n => basket.BasketItems.Select(x => x.ProductId).Contains(n.Id))
                            .ToList();


            var dtos = from p in products
                       join bi in (basket?.BasketItems ?? []) on p.Id equals bi.ProductId
                       select new BasketItemDto
                       {

                           Id = bi.Id,
                           ProductId = p.Id,
                           ProductName = p.Name,
                           Quantity = bi.Quantity,
                           ProductPrice = p.Price,
                           Product = new ProductDto
                           {
                               Id = p.Id,
                               Name = p.Name,
                               SKU = p.SKU,
                               Description = p.Description,
                               Price = p.Price,
                           }
                       };

            return Result<BasketDto>.Success(new BasketDto
            {
                CustomerEmail = basket.CustomerEmail,
                BasketItems = dtos.ToList()
            });            
        }
    }
}
