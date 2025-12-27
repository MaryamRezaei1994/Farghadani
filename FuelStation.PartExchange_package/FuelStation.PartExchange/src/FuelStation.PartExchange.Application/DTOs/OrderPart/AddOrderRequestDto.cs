using System.ComponentModel.DataAnnotations;
using FuelStation.PartExchange.Domain.Enums;

namespace FuelStation.PartExchange.Application.DTOs.OrderPart;

public abstract class AddOrderRequestDto
{
    [Required(ErrorMessage = "The requestingStationId field is required")]
    public Guid RequestingStationId { get; set; }
    
    [Required(ErrorMessage = "The SupplierStationId field is required")]
    public Guid SupplierStationId { get; set; }
    
    [Required(ErrorMessage = "The PartId field is required")]
    public Guid PartId { get; set; }
    
    [Required(ErrorMessage = "The Quantity field is required")]
    public int Quantity { get; set; }
    
    public OrderStatus? Status { get; set; }
}