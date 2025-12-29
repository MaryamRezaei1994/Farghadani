using FuelStation.PartExchange.Application.DTOs.OrderPart;
using Microsoft.AspNetCore.Mvc;
using FuelStation.PartExchange.Application.Services;
// authorization handled by API Gateway

namespace FuelStation.PartExchange.WebApi.Controllers;

/// <summary>
/// API controller for creating part requests from stations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class OrderPartController : ControllerBase
{
    private readonly OrderPartService _service;
    /// <summary>
    /// Initializes a new instance of <see cref="OrderPartController"/>.
    /// </summary>
    /// <param name="service">The part request service.</param>
    public OrderPartController(OrderPartService service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> CreateOrderPart([FromBody]AddOrderRequestDto dto,string userName)
    {
        var request = await _service.AddOrder(dto, userName);
        return Ok(request);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOrderPartById([FromBody]GetOrderByIdRequestDto dto,string userName)
    {
        var request = await _service.GetOrderById(dto.Id, userName);
        return Ok(request);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllOrderPart([FromBody]GetAllOrdersRequestDto dto,string userName)
    {
        var request = await _service.GetAllOrders(dto, userName);
        return Ok(request);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateOrderPart([FromBody]UpdateOrderRequestDto dto,string userName)
    {
        var request = await _service.UpdateOrder(dto, userName);
        return Ok(request);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteOrderPart([FromBody]DeleteOrderByIdRequestDto dto,string userName)
    {
        var request = await _service.DeleteOrderById(dto.Id, userName);
        return Ok(request);
    }
    
    
     /// <summary>
     /// Confirms the specified order.
     /// </summary>
     /// <param name="id">Order identifier.</param>
     [HttpPost("{id:guid}")]
     public async Task<IActionResult> ConfirmOrder([FromBody]GetOrderByIdRequestDto dto,string userName)
     {
         var request = await _service.ConfirmOrder(dto.Id, userName);
         return Ok(request);
     }
}
