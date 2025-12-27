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
}
