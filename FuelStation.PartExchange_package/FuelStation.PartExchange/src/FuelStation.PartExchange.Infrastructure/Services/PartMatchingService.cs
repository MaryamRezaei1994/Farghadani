using FuelStation.PartExchange.Domain.Interfaces;
using FuelStation.PartExchange.Domain.Entities;

namespace FuelStation.PartExchange.Infrastructure.Services;

/// <summary>
/// Service that finds supplier stations which have requested parts in stock.
/// </summary>
public class PartMatchingService : IPartMatchingService
{
    private readonly IFuelStationRepository _stationRepo;
    private readonly IPartInventoryRepository _inventoryRepo;

    /// <summary>
    /// Initializes a new instance of <see cref="PartMatchingService"/>.
    /// </summary>
    public PartMatchingService(IFuelStationRepository stationRepo, IPartInventoryRepository inventoryRepo)
    {
        _stationRepo = stationRepo;
        _inventoryRepo = inventoryRepo;
    }

    /// <summary>
    /// Finds supplier stations in the same city that have sufficient quantity of the requested part.
    /// </summary>
    /// <param name="requestingStationId">The requesting station identifier.</param>
    /// <param name="partNumber">The part number to find.</param>
    /// <param name="quantity">Required quantity.</param>
    /// <returns>Enumerable of station and inventory tuples that can supply the part.</returns>
    public async Task<IEnumerable<(Domain.Entities.FuelStation Station, StationInventory Inventory)>> FindSuppliersAsync(Guid requestingStationId, string partNumber, int quantity)
    {
        var requesting = await _stationRepo.GetByIdAsync(requestingStationId);
        if (requesting == null) return Enumerable.Empty<(Domain.Entities.FuelStation, StationInventory)>();

        var candidates = await _inventoryRepo.FindPartInCityAsync(requesting.City, partNumber);
        return candidates.Where(x => x.Inventory.Quantity >= quantity && x.Station.Id != requestingStationId);
    }
}
