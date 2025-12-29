using MemoryPack;

namespace FuelStation.PartExchange.Domain.Models;
[MemoryPackable]
public partial class FuelStation : BaseModel
{
    public string Name { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Address { get; set; } = null!;
}
