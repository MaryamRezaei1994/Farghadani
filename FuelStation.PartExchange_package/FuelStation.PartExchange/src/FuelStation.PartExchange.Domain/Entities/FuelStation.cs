namespace FuelStation.PartExchange.Domain.Entities;

public class FuelStation
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Address { get; set; } = null!;
}
