namespace FuelStation.PartExchange.Domain.Entities;
//اطلاعات مربوط به فاکتور
public class Invoice
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    //مبلغ کل فاکتور
    public decimal TotalAmount { get; set; }
    //زمان صدور فاکتور
    public DateTime IssuedAt { get; set; }
}
