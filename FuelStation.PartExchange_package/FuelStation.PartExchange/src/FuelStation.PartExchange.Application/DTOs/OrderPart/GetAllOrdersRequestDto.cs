namespace FuelStation.PartExchange.Application.DTOs.OrderPart;

public abstract class GetAllOrdersRequestDto
{
    public int Page { get; set; } = 1;
    public int PerPage { get; set; } = 10;
}