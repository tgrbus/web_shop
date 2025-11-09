using FluentValidation;
using Grbus.WebShop.Application.Baskets.Commands;

namespace Grbus.WebShop.Application.Baskets.Validators;

public class ChangeBasketItemQuantityCommandValidator : AbstractValidator<ChangeBasketItemQuantityCommand>
{
    public ChangeBasketItemQuantityCommandValidator()
    {
        RuleFor(v => v.CustomerEmail).NotEmpty().EmailAddress().WithMessage("Customer email must be valid email address");
        RuleFor(v => v.ProductId).NotEmpty().WithMessage("ProductID is required");
        RuleFor(v => v.Quantity).NotEmpty().WithMessage("Quantity is required");
    }
}
