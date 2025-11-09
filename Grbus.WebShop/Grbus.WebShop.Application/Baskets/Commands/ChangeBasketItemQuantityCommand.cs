using Grbus.WebShop.Application.Common;
using Grbus.WebShop.Domain.Aggregates.Baskets.Repository;
using Grbus.WebShop.Domain.Common;
using MediatR;

namespace Grbus.WebShop.Application.Baskets.Commands
{
    public record ChangeBasketItemQuantityCommand : IRequest<Result>
    {
        public required string CustomerEmail { get; init; }
        public int ProductId { get; init; }
        public int Quantity { get; init; }
    }

    public class ChangeBasketItemQuantityCommandHandler : IRequestHandler<ChangeBasketItemQuantityCommand, Result>
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeBasketItemQuantityCommandHandler(IBasketRepository basketRepo, IUnitOfWork unitOfWork)
        {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(ChangeBasketItemQuantityCommand command, CancellationToken ct)
        {
            var basket = await _basketRepo.GetBasketByIdAsync(command.CustomerEmail);
            if(basket == null)
            {
                return Result.Failure(ApplicationErrors.BasketNotFound);
            }

            basket.IncreaseOrDecreaseQuantity(command.ProductId, command.Quantity);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
