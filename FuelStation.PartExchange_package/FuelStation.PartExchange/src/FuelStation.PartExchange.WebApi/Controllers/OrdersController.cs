using Microsoft.AspNetCore.Mvc;
using FuelStation.PartExchange.Domain.Interfaces;
using FuelStation.PartExchange.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace FuelStation.PartExchange.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _orderRepo;
    private readonly IUnitOfWork _uow;

    public OrdersController(IOrderRepository orderRepo, IUnitOfWork uow)
    {
        _orderRepo = orderRepo;
        _uow = uow;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(new { message = "Not implemented: list orders" });

    [HttpPost("{id:guid}/confirm")]
    [Authorize(Policy = "SupplierOnly")]
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
