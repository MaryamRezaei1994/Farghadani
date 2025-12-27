using FuelStation.PartExchange.Domain.Interfaces;
using FuelStation.PartExchange.Domain.Models;

namespace FuelStation.PartExchange.Infrastructure.Services;

/// <summary>
/// Service that finds supplier stations which have requested parts in stock.
/// </summary>
public class PartMatchingService : IPartMatchingService
{
    private readonly IFuelStationRepository _fuelStationRepository;
    private readonly IPartInventoryRepository _inventoryRepo;

    /// <summary>
    /// Initializes a new instance of <see cref="PartMatchingService"/>.
    /// </summary>
    public PartMatchingService(IPartInventoryRepository inventoryRepo, IFuelStationRepository fuelStationRepository)
    {
        _inventoryRepo = inventoryRepo;
        _fuelStationRepository = fuelStationRepository;
    }

    /// Finds supplier stations in the same city that have sufficient quantity of the requested part.
    public async Task<List<(Domain.Models.FuelStation Station, StationInventory Inventory)>> FindPartInCityAsync(
        Guid requestingStationId, int quantity, string partNumber)
    {
        var conditionList = new List<(Domain.Models.FuelStation Station, StationInventory Inventory)>();
        var requesting = new Domain.Models.FuelStation();
        
        requesting = await _fuelStationRepository.GetByIdAsync(requestingStationId);
        if (requesting is null)
        {
            return null;
        }
        conditionList = await _inventoryRepo.FindPartInCityAsync(requesting.City, partNumber);
        if (conditionList is null)
        {
            return conditionList.Where(x => x.Inventory.Quantity >= quantity && x.Station.Id != requestingStationId) as
                List<(Domain.Models.FuelStation Station, StationInventory Inventory)>;
        }
        return conditionList;
        
    }
}