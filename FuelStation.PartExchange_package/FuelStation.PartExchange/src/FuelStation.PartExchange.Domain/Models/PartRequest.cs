using FuelStation.PartExchange.Domain.Enums;

namespace FuelStation.PartExchange.Domain.Models;

public class PartRequest
{
    public Guid Id { get; set; }
    public Guid StationId { get; set; }
    //the name of the part
    public string PartNumber { get; set; }
    public int Quantity { get; set; }
    public PartRequestStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
