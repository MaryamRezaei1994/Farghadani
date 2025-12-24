namespace FuelStation.PartExchange.Domain.Entities;
//نگه داری موجودی یک قطعه برای یک ایستگاه سوخت
public class StationInventory
{
    public Guid StationId { get; set; }
    public Guid PartId { get; set; }
    public int Quantity { get; set; }
}
