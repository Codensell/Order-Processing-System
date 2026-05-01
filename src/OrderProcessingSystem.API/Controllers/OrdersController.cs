using Microsoft.AspNetCore.Mvc;
using OrderProcessingSystem.Application.Orders.CreateOrder;

namespace OrderProcessingSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class OrdersController : ControllerBase
{
    private readonly ICreateOrderService _createOrderService;
    public OrdersController(ICreateOrderService createOrderService)
    {
        _createOrderService = createOrderService;
    }
    [HttpPost]
    public IActionResult CreateOrder([FromBody] CreateOrderRequest request)
    {
        var result = _createOrderService.Execute(request);
        return Ok(result);
    }
}