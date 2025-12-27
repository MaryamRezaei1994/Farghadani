using System.ComponentModel.DataAnnotations.Schema;

namespace FuelStation.PartExchange.Domain.Models;
public class EmbeddedMapping
{
    public Guid Id { get; set; }
    public string Key { get; set; } = null!;
    [Column(TypeName = "jsonb")]
    public Dictionary<string, object> Data { get; set; } = new();

}