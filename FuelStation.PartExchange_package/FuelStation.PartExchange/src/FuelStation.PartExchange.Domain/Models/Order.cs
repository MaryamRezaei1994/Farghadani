using FuelStation.PartExchange.Domain.Enums;
using MemoryPack;

namespace FuelStation.PartExchange.Domain.Models;

[MemoryPackable]
public partial class Order : BaseModel
{
    public Guid RequestingStationId { get; set; }
    public Guid SupplierStationId { get; set; }
    public Guid PartId { get; set; }
    public int Quantity { get; set; }
    public OrderStatus? Status { get; set; }
}
