namespace FuelStation.PartExchange.Domain.Entities;

public class StationInventory
{
    public Guid StationId { get; set; }
    public Guid PartId { get; set; }
    public int Quantity { get; set; }
}
