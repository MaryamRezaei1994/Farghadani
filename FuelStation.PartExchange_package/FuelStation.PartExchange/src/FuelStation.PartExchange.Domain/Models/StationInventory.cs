using MemoryPack;

namespace FuelStation.PartExchange.Domain.Models;
//نگه داری موجودی یک قطعه برای یک ایستگاه سوخت
[MemoryPackable]
public partial class StationInventory : BaseModel
{
    public Guid StationId { get; set; }
    public Guid PartId { get; set; }
    public int Quantity { get; set; }
}
