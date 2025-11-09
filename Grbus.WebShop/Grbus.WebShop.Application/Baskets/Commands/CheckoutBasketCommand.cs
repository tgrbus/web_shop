using Grbus.WebShop.Application.Common;
using Grbus.WebShop.Domain.Aggregates.Baskets.Repository;
using Grbus.WebShop.Domain.Aggregates.Baskets.Services;
using Grbus.WebShop.Domain.Common;
using MediatR;

namespace Grbus.WebShop.Application.Baskets.Commands;

public record CheckoutBasketCommand : IRequest<Result>
{
    public required string Email { get; init; }
}

public class CheckoutBaskeCommandHandler : IRequestHandler<CheckoutBasketCommand, Result>
{
    private readonly IBasketRepository _basketRepo;
    private readonly CheckoutService _checkoutService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICachingService _cachingService;

    public CheckoutBaskeCommandHandler(
        IBasketRepository basketRepo,
        CheckoutService checkoutService,
        IUnitOfWork unitOfWork,
        ICachingService cachingService
        )
    {
        _basketRepo = basketRepo;
        _checkoutService = checkoutService;
        _unitOfWork = unitOfWork;
        _cachingService = cachingService;
    }

    public async Task<Result> Handle(CheckoutBasketCommand command, CancellationToken ct)
    {
        _unitOfWork.CreateTransaction();
        Result? result = null;

        var basket = await _basketRepo.GetBasketByIdAsync(command.Email);
        if (basket == null)
        {
            result = Result.Failure(ApplicationErrors.BasketNotFound);
        }
        else
        {
            foreach(var item in basket.BasketItems)
            {
                await _cachingService.Remove($"product_{item.ProductId}");
            }
            result = await _checkoutService.CheckoutBasketAsync(basket);
        }

        if (result.IsFailure)
        {
            _unitOfWork.RollbackTransaction();
        }
        else
        {
            await _unitOfWork.CommitTransaction();            
        }

        return result;
    }
}

