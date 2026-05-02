using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderProcessingSystem.Application.Orders.CreateOrder;
using OrderProcessingSystem.Application.Orders.GetOrderById;
using OrderProcessingSystem.Application.Orders.GetOrders;

namespace OrderProcessingSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ICreateOrderService _createOrderService;
    private readonly IGetOrderByIdService _getOrderByIdService;
    private readonly IGetOrdersService _getOrdersService;
    public OrdersController(
        ICreateOrderService createOrderService,
        IGetOrderByIdService getOrderByIdService,
        IGetOrdersService getOrdersService)
    {
        _createOrderService = createOrderService;
        _getOrderByIdService = getOrderByIdService;
        _getOrdersService = getOrdersService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var result = await _createOrderService.ExecuteAsync(request, cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders(CancellationToken cancellationToken)
    {
        var result = await _getOrdersService.ExecuteAsync(cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOrderById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _getOrderByIdService.ExecuteAsync(id, cancellationToken);
        if (result == null)
            return NotFound();

        return Ok(result);
    }
}