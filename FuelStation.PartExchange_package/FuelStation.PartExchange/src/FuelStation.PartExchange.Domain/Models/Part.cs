using MemoryPack;

namespace FuelStation.PartExchange.Domain.Models;

[MemoryPackable]
public partial class Part :BaseModel
{
    public string PartNumber { get; set; } = null!; // unique
    public string Name { get; set; } = null!;
}
