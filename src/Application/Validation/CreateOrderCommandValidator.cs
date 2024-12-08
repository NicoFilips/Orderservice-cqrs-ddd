using FluentValidation;
using OrderService.Application.Commands.CreateOrder;

namespace OrderService.Application.Validation;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.CustomerId)
           .NotEmpty()
           .WithMessage("CustomerId must not be empty.");

        RuleFor(x => x.Item)
           .NotNull()
           .WithMessage("Order item must not be null.");

        RuleFor(x => x.Item.ProductId)
           .NotEmpty()
           .WithMessage("ProductId must not be empty.");

        RuleFor(x => x.Item.Quantity)
           .GreaterThan(0)
           .WithMessage("Quantity must be greater than zero.");
    }
}
