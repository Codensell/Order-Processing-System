using Microsoft.AspNetCore.Mvc;
using OrderProcessingSystem.Application.Orders.CreateOrder;
using OrderProcessingSystem.Application.Orders.GetOrderById;

namespace OrderProcessingSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ICreateOrderService _createOrderService;
    private readonly IGetOrderByIdService _getOrderByIdService;
    public OrdersController(
        ICreateOrderService createOrderService,
        IGetOrderByIdService getOrderByIdService)
    {
        _createOrderService = createOrderService;
        _getOrderByIdService = getOrderByIdService;
    }
    [HttpPost]
    public IActionResult CreateOrder([FromBody] CreateOrderRequest request)
    {
        var result = _createOrderService.Execute(request);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetOrderById(Guid id)
    {
        var result = _getOrderByIdService.Execute(id);
        if (result == null)
            return NotFound();

        return Ok(result);
    }
}