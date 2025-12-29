using MemoryPack;

namespace FuelStation.PartExchange.Domain.Models;
//اطلاعات مربوط به فاکتور
[MemoryPackable]
public partial class Invoice : BaseModel
{
    public Guid OrderId { get; set; }
    //مبلغ کل فاکتور
    public decimal TotalAmount { get; set; }
    //زمان صدور فاکتور
    public DateTime IssuedAt { get; set; }
}
