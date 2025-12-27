namespace FuelStation.PartExchange.Domain.Models;

public class Part
{
    public Guid Id { get; set; }
    public string PartNumber { get; set; } = null!; // unique
    public string Name { get; set; } = null!;
}
