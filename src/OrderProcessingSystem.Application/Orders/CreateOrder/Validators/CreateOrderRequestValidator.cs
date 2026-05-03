using FluentValidation;

namespace OrderProcessingSystem.Application.Orders.CreateOrder.Validators;

public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(request => request.Items)
            .NotEmpty()
            .WithMessage("Order must contain at least one item");

        RuleForEach(request => request.Items)
            .ChildRules(item =>
            {
                item.RuleFor(x => x.ProductName)
                    .NotEmpty()
                    .WithMessage("Product name is required");

                item.RuleFor(x => x.Price)
                    .GreaterThan(0)
                    .WithMessage("Price must be greater than zero");

                item.RuleFor(x => x.Quantity)
                    .GreaterThan(0)
                    .WithMessage("Quantity must be greater than zero");
            });
    }
}