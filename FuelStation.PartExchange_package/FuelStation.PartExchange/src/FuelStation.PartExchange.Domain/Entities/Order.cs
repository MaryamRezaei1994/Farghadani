using FuelStation.PartExchange.Domain.Enums;

namespace FuelStation.PartExchange.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }
    public Guid RequestingStationId { get; set; }
    public Guid SupplierStationId { get; set; }
    public Guid PartId { get; set; }
    public int Quantity { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
