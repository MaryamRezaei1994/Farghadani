using Microsoft.AspNetCore.Mvc;
using FuelStation.PartExchange.Application.Services;
// authorization handled by API Gateway

namespace FuelStation.PartExchange.WebApi.Controllers;

/// <summary>
/// API controller for creating part requests from stations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PartRequestsController : ControllerBase
{
    private readonly PartRequestService _service;
    /// <summary>
    /// Initializes a new instance of <see cref="PartRequestsController"/>.
    /// </summary>
    /// <param name="service">The part request service.</param>
    public PartRequestsController(PartRequestService service) => _service = service;

    [HttpPost]
    /// <summary>
    /// Creates a new part request.
    /// </summary>
    /// <param name="dto">The create request DTO.</param>
    /// <returns>The created <see cref="FuelStation.PartExchange.Domain.Entities.PartRequest"/>.</returns>
    public async Task<IActionResult> Create([FromBody] CreatePartRequestDto dto)
    {
        var request = await _service.CreatePartRequestAsync(dto.RequestingStationId, dto.PartNumber, dto.Quantity);
        return Ok(request);
    }
}

/// <summary>
/// DTO for creating a part request.
/// </summary>
public record CreatePartRequestDto(Guid RequestingStationId, string PartNumber, int Quantity);
