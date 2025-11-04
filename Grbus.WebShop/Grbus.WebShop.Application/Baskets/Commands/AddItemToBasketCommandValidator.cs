using FluentValidation;

namespace Grbus.WebShop.Application.Baskets.Commands
{
    public class AddItemToBasketCommandValidator : AbstractValidator<AddItemsToBasketCommand>
    {
        public AddItemToBasketCommandValidator()
        {
            RuleFor(n => n.CustomerEmail).NotEmpty().EmailAddress().WithMessage("Customer email is required");
            RuleFor(n => n.ProductId).NotNull().WithMessage("Product ID is requiered");
            RuleFor(n => n.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero");
        }
    }
}
