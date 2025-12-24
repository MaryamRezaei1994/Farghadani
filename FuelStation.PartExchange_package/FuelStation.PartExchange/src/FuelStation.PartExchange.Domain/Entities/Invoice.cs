namespace FuelStation.PartExchange.Domain.Entities;

public class Invoice
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime IssuedAt { get; set; }
}
