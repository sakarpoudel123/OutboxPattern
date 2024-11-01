using Microsoft.AspNetCore.Mvc;

namespace Outbox_Pattern_Demo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    public OrderController(IOrderService orderService){
        _orderService = orderService;
    }
    [HttpGet("GetOrders")]
    public async Task<List<Order>> GetOrders(){
        return await _orderService.GetAllOrdersAsync();
    }
    [HttpGet("{id}")]
    public async Task<Order> GetOrder(int id){
        return await _orderService.GetOrderAsync(id);
    }
    [HttpPost("CreateOrder")]
    public async Task<IActionResult> CreateOrder([FromBody] Order order){

        await _orderService.CreateOrderAsync(order);
        return Ok();
    }
}
