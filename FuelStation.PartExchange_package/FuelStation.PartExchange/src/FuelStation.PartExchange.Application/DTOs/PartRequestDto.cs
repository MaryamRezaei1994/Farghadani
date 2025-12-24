namespace FuelStation.PartExchange.Application.DTOs;

public record PartRequestDto(Guid Id, Guid RequestingStationId, string PartNumber, int Quantity, string Status, DateTime CreatedAt);
