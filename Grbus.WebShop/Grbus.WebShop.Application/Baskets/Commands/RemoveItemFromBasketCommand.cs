using Grbus.WebShop.Application.Common;
using Grbus.WebShop.Domain.Aggregates.Baskets.Repository;
using Grbus.WebShop.Domain.Common;
using MediatR;

namespace Grbus.WebShop.Application.Baskets.Commands
{
    public record RemoveItemFromBasketCommand : IRequest<Result>
    {
        public required string CustomerEmail { get; init; }
        public int ProductId { get; init; }
    }

    public class RemoveItemFromBasketCommandHandler : IRequestHandler<RemoveItemFromBasketCommand, Result>
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveItemFromBasketCommandHandler(IBasketRepository basketRepo, IUnitOfWork unitOfWork)
        {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(RemoveItemFromBasketCommand command, CancellationToken ct)
        {
            var basket = await _basketRepo.GetBasketByIdAsync(command.CustomerEmail);
            if (basket == null)
            {
                return Result.Failure(ErrorLists.BasketNotFound);
            }
            basket.RemoveProduct(command.ProductId);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
