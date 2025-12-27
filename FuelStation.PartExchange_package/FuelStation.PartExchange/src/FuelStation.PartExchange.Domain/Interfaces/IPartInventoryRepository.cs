using FuelStation.PartExchange.Domain.Models;

namespace FuelStation.PartExchange.Domain.Interfaces;

public interface IPartInventoryRepository
{
    Task<StationInventory?> GetInventoryAsync(Guid stationId, string partNumber);
    Task<List<(Models.FuelStation Station, StationInventory Inventory)>> FindPartInCityAsync(string city, string partNumber);
    Task DecreaseInventoryAsync(Guid stationId, Guid partId, int quantity);
    Task AddInventoryAsync(StationInventory inventory);
}
