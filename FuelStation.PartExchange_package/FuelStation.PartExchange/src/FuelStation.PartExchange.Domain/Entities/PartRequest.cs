using FuelStation.PartExchange.Domain.Enums;

namespace FuelStation.PartExchange.Domain.Entities;

public class PartRequest
{
    public Guid Id { get; set; }
    public Guid RequestingStationId { get; set; }
    public string PartNumber { get; set; } = null!;
    public int Quantity { get; set; }
    public PartRequestStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
