using Grbus.WebShop.Domain.Aggregates.Products;
using Grbus.WebShop.Domain.Common;

namespace Grbus.WebShop.Domain.Aggregates.Baskets.Services
{
    public class CheckoutService
    {
        private readonly IProductRepository _productRepo;

        public CheckoutService(IProductRepository productRepo) => _productRepo = productRepo;

        public async Task<Result> CheckoutBasketAsync(Basket basket)
        {
            foreach(var item in basket.BasketItems)
            {
                var product = await _productRepo.GetByIdAsync(item.ProductId);
                var result = product!.ChangeStockQuantity(-item.Quantity);
                if(result.IsFailure)
                {
                    return result;
                }
                basket.RemoveProduct(item.ProductId);
            }
            return Result.Success();
        }
    }
}
