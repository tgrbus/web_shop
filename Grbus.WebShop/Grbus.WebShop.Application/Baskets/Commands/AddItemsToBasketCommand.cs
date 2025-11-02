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

        public async Task<Result> Handle(AddItemsToBasketCommand request, CancellationToken cancellationToken)
        {
            // Implementation to add items to basket goes here.
            // This is a placeholder implementation.
            return await Task.FromResult(Result.Success());
        }
    }
}
