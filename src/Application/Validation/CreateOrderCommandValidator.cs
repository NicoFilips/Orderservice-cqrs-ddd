using FluentValidation;
using OrderService.Application.Commands.CreateOrder;

namespace OrderService.Application.Validation;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor<object>(x => x.CustomerId).NotEmpty();
        RuleFor<object>(x => x.Item).NotEmpty().WithMessage("Order must contain an item.");
    }
}
