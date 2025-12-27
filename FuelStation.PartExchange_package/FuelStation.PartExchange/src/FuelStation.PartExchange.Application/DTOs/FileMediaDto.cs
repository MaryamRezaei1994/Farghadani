using MemoryPack;

namespace FuelStation.PartExchange.Application.DTOs;
[MemoryPackable]
public partial class FileMediaDto
{
    public Guid Id { get; set; }
    public Guid ObjectId { get; set; }
    public string DeviceModel { get; set; } = null!;
    public string ClientType { get; set; } = null!;
    public string Url { get; set; } = null!;
}
