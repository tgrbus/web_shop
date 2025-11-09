using FluentValidation;
using Grbus.WebShop.Application.Baskets.Commands;

namespace Grbus.WebShop.Application.Baskets.Validators;

public class AddItemsToBasketCommandValidator : AbstractValidator<AddItemsToBasketCommand>
{
    public AddItemsToBasketCommandValidator()
    {
        RuleFor(n => n.CustomerEmail).NotEmpty().EmailAddress().WithMessage("Customer email is required");
        RuleFor(n => n.ProductId).GreaterThan(0).WithMessage("Greter than zero").NotNull().WithMessage("Product ID is requiered");
        RuleFor(n => n.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero");
    }
}

