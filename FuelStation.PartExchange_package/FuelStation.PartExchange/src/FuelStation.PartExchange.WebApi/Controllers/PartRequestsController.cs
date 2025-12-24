using Microsoft.AspNetCore.Mvc;
using FuelStation.PartExchange.Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace FuelStation.PartExchange.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PartRequestsController : ControllerBase
{
    private readonly PartRequestService _service;

    public PartRequestsController(PartRequestService service) => _service = service;

    [HttpPost]
    [Authorize(Policy = "OperatorOnly")]
    public async Task<IActionResult> Create([FromBody] CreatePartRequestDto dto)
    {
        var request = await _service.CreatePartRequestAsync(dto.RequestingStationId, dto.PartNumber, dto.Quantity);
        return Ok(request);
    }
}

public record CreatePartRequestDto(Guid RequestingStationId, string PartNumber, int Quantity);
