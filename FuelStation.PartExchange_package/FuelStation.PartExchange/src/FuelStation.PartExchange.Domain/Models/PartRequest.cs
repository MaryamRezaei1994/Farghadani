using FuelStation.PartExchange.Domain.Enums;
using MemoryPack;

namespace FuelStation.PartExchange.Domain.Models;

[MemoryPackable]
public partial class PartRequest : BaseModel
{
    public Guid StationId { get; set; }

    //the name of the part
    public string PartNumber { get; set; }
    public int Quantity { get; set; }
    public PartRequestStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}