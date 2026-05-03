using OrderProcessingSystem.Domain.Entities;
using OrderProcessingSystem.Application.Orders;
using FluentValidation;

namespace OrderProcessingSystem.Application.Orders.CreateOrder;

public class CreateOrderService : ICreateOrderService
{
    private readonly IOrderRepository _repository;
    private readonly IValidator<CreateOrderRequest> _validator;

    public CreateOrderService(IOrderRepository repository, IValidator<CreateOrderRequest> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<CreateOrderResponse> ExecuteAsync(
        CreateOrderRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new FluentValidation.ValidationException(validationResult.Errors);
        
        var order = Order.Create();

        foreach (var item in request.Items)
        {
            order.AddItem(item.ProductName, item.Price, item.Quantity);
        }

        _repository.Add(order);
        await _repository.SaveChangesAsync(cancellationToken);

        return new CreateOrderResponse
        {
            OrderId = order.Id
        };
    }
}
