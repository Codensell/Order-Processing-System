using FluentValidation;
using OrderProcessingSystem.Application.Orders.Events;
using OrderProcessingSystem.Domain.Entities;

namespace OrderProcessingSystem.Application.Orders.CreateOrder;

public class CreateOrderService : ICreateOrderService
{
    private readonly IOrderRepository _repository;
    private readonly IValidator<CreateOrderRequest> _validator;
    private readonly IOrderEventPublisher _eventPublisher;

    public CreateOrderService(
        IOrderRepository repository,
        IValidator<CreateOrderRequest> validator,
        IOrderEventPublisher eventPublisher)
    {
        _repository = repository;
        _validator = validator;
        _eventPublisher = eventPublisher;
    }

    public async Task<CreateOrderResponse> ExecuteAsync(
        CreateOrderRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new FluentValidation.ValidationException(validationResult.Errors);
        }

        var order = Order.Create();

        foreach (var item in request.Items)
        {
            order.AddItem(item.ProductName, item.Price, item.Quantity);
        }

        _repository.Add(order);
        await _repository.SaveChangesAsync(cancellationToken);

        await _eventPublisher.PublishOrderCreatedAsync(
            new OrderCreatedEvent
            {
                OrderId = order.Id,
                CreatedAt = order.CreatedAt,
                ItemsCount = order.Items.Count
            },
            cancellationToken);

        return new CreateOrderResponse
        {
            OrderId = order.Id
        };
    }
}