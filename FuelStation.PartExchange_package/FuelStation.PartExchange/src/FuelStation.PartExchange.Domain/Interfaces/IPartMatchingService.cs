using FuelStation.PartExchange.Domain.Entities;

namespace FuelStation.PartExchange.Domain.Interfaces;

public interface IPartMatchingService
{
    Task<IEnumerable<(Entities.FuelStation Station, StationInventory Inventory)>> FindSuppliersAsync(Guid requestingStationId, string partNumber, int quantity);
}
