using FuelStation.PartExchange.Application.DTOs.OrderPart;
using Microsoft.AspNetCore.Mvc;
using FuelStation.PartExchange.Application.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace FuelStation.PartExchange.WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
[Tags("Part Orders")]
public class OrderPartController(OrderPartService service) : ControllerBase
{
    [HttpPost("[action]")]
    [SwaggerOperation(OperationId = "CreateOrderPart")]
    public async Task<IActionResult> CreateOrderPart([FromBody] AddOrderRequestDto dto, string userName)
    {
        var request = await service.AddOrder(dto, userName);
        return Ok(request);
    }

    [HttpGet("[action]")]
    [SwaggerOperation(OperationId = "GetOrderPartById")]
    public async Task<IActionResult> GetOrderPartById([FromBody] GetOrderByIdRequestDto dto, string userName)
    {
        var request = await service.GetOrderById(dto.Id, userName);
        return Ok(request);
    }

    [HttpGet("[action]")]
    [SwaggerOperation(OperationId = "GetAllOrderPart")]
    public async Task<IActionResult> GetAllOrderPart([FromBody] GetAllOrdersRequestDto dto, string userName)
    {
        var request = await service.GetAllOrders(dto, userName);
        return Ok(request);
    }

    [HttpPut("[action]")]
    [SwaggerOperation(OperationId = "UpdateOrderPart")]
    public async Task<IActionResult> UpdateOrderPart([FromBody] UpdateOrderRequestDto dto, string userName)
    {
        var request = await service.UpdateOrder(dto, userName);
        return Ok(request);
    }

    [HttpDelete("[action]")]
    [SwaggerOperation(OperationId = "DeleteOrderPart")]
    public async Task<IActionResult> DeleteOrderPart([FromBody] DeleteOrderByIdRequestDto dto, string userName)
    {
        var request = await service.DeleteOrderById(dto.Id, userName);
        return Ok(request);
    }

    [HttpPost("[action]")]
    [SwaggerOperation(OperationId = "ConfirmOrder")]
    public async Task<IActionResult> ConfirmOrder([FromBody] GetOrderByIdRequestDto dto, string userName)
    {
        var request = await service.ConfirmOrder(dto.Id, userName);
        return Ok(request);
    }
}