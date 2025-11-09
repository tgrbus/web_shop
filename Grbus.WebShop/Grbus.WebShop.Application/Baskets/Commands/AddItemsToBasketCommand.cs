using Grbus.WebShop.Application.Common;
using Grbus.WebShop.Domain.Aggregates.Baskets.Repository;
using Grbus.WebShop.Domain.Common;
using MediatR;

namespace Grbus.WebShop.Application.Baskets.Commands
{
    public record AddItemsToBasketCommand : IRequest<Result>
    {
        public required string CustomerEmail { get; init; }
        public int ProductId { get; init; }
        public int Quantity { get; init; }
    }

    public class AddItemsToBasketCommandHandler : IRequestHandler<AddItemsToBasketCommand, Result>
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;

        public AddItemsToBasketCommandHandler(IBasketRepository basketRepo, IUnitOfWork unitOfWork)
        {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(AddItemsToBasketCommand request, CancellationToken cancellationToken)
        {
            var basket = await _basketRepo.GetBasketByIdAsync(request.CustomerEmail);
            if (basket == null)
            {
                return Result.Failure(ApplicationErrors.BasketNotFound);
            }

            basket.AddProduct(request.ProductId, request.Quantity);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
