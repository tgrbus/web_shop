using FluentValidation;
using Grbus.WebShop.Application.Baskets.Commands;

namespace Grbus.WebShop.Application.Baskets.Validators;

public class RemoveItemFromBasketCommandValidator : AbstractValidator<RemoveItemFromBasketCommand>
{
    public RemoveItemFromBasketCommandValidator()
    {
        RuleFor(v => v.CustomerEmail).NotEmpty().EmailAddress().WithMessage("Customer email must be valid email address");
        RuleFor(v => v.ProductId).NotEmpty().WithMessage("ProductID is required");
    }    
}

