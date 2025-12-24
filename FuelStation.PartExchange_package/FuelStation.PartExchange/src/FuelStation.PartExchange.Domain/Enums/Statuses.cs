namespace FuelStation.PartExchange.Domain.Enums;

public enum PartRequestStatus
{
    Created,
    Matched,
    Completed,
    Cancelled
}

public enum OrderStatus
{
    Pending,
    Confirmed,
    Invoiced,
    Shipped
}
