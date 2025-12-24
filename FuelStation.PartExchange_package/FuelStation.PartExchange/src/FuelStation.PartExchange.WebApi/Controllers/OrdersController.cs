using Microsoft.AspNetCore.Mvc;
using FuelStation.PartExchange.Domain.Interfaces;
using FuelStation.PartExchange.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace FuelStation.PartExchange.WebApi.Controllers;

/// <summary>
/// Controller for managing orders related to part requests.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _orderRepo;
    private readonly IUnitOfWork _uow;

    /// <summary>
    /// Initializes a new instance of <see cref="OrdersController"/>.
    /// </summary>
    public OrdersController(IOrderRepository orderRepo, IUnitOfWork uow)
    {
        _orderRepo = orderRepo;
        _uow = uow;
    }

    [HttpGet]
    /// <summary>
    /// Gets all orders. (Not implemented)
    /// </summary>
    public async Task<IActionResult> GetAll() => Ok(new { message = "Not implemented: list orders" });

    [HttpPost("{id:guid}/confirm")]
    [Authorize(Policy = "SupplierOnly")]
    /// <summary>
    /// Confirms the specified order.
    /// </summary>
    /// <param name="id">Order identifier.</param>
    public async Task<IActionResult> Confirm(Guid id)
    {
        var order = await _orderRepo.GetByIdAsync(id);
        if (order == null) return NotFound();
        order.Status = OrderStatus.Confirmed;
        await _orderRepo.UpdateAsync(order);
        await _uow.SaveChangesAsync();
        return Ok(order);
    }
}
