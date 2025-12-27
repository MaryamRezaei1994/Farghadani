using System.ComponentModel.DataAnnotations;
using MemoryPack;

namespace FuelStation.PartExchange.Domain.Models;

[MemoryPackable]
public partial class BaseModel
{
    [Key] public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
}