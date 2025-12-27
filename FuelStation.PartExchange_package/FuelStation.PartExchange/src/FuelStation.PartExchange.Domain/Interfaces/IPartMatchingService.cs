using FuelStation.PartExchange.Domain.Models;

namespace FuelStation.PartExchange.Domain.Interfaces;

public interface IPartMatchingService
{
    Task<List<(Models.FuelStation Station, StationInventory Inventory)>> FindPartInCityAsync(Guid requestingStationId,
        int quantity, string partNumber);
}