using Microsoft.AspNetCore.Mvc;
using OrderProcessingSystem.Application.Orders.CreateOrder;

namespace OrderProcessingSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class OrdersController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateOrder([FromBody] CreateOrderRequest request)
    {
        var service = new CreateOrderService();
        var result = service.Execute(request);

        return Ok(result);
    }
}