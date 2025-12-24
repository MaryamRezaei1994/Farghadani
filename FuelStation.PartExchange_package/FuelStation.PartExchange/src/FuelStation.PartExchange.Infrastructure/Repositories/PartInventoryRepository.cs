using FuelStation.PartExchange.Domain.Entities;
using FuelStation.PartExchange.Domain.Interfaces;
using FuelStation.PartExchange.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FuelStation.PartExchange.Infrastructure.Repositories;

/// <summary>
/// Repository for managing station inventory and parts.
/// </summary>
public class PartInventoryRepository : IPartInventoryRepository
{
    private readonly PartExchangeDbContext _db;

    public PartInventoryRepository(PartExchangeDbContext db) => _db = db;

    /// <summary>
    /// Adds inventory for a station.
    /// </summary>
    /// <param name="inventory">Station inventory entity.</param>
    public async Task AddInventoryAsync(StationInventory inventory)
    {
        await _db.StationInventories.AddAsync(inventory);
    }

    /// <summary>
    /// Decreases inventory quantity for a given station and part.
    /// </summary>
    public async Task DecreaseInventoryAsync(Guid stationId, Guid partId, int quantity)
    {
        var inv = await _db.StationInventories.FindAsync(new object[] { stationId, partId });
        if (inv == null) return;
        inv.Quantity -= quantity;
    }

    /// <summary>
    /// Retrieves inventory for a specific station and part number.
    /// </summary>
    public async Task<StationInventory?> GetInventoryAsync(Guid stationId, string partNumber)
    {
        var part = await _db.Parts.FirstOrDefaultAsync(p => p.PartNumber == partNumber);
        if (part == null) return null;
        return await _db.StationInventories.FindAsync(new object[] { stationId, part.Id }) as StationInventory;
    }

    /// <summary>
    /// Finds stations in the specified city that have the given part in stock.
    /// </summary>
    public async Task<IEnumerable<(Domain.Entities.FuelStation Station, StationInventory Inventory)>> FindPartInCityAsync(string city, string partNumber)
    {
        var part = await _db.Parts.FirstOrDefaultAsync(p => p.PartNumber == partNumber);
        if (part == null) return Enumerable.Empty<(Domain.Entities.FuelStation, StationInventory)>();

        var query = from s in _db.FuelStations
                    join inv in _db.StationInventories on s.Id equals inv.StationId
                    where s.City == city && inv.PartId == part.Id && inv.Quantity > 0
                    select new { Station = s, Inventory = inv };

        var list = await query.ToListAsync();
        return list.Select(x => (x.Station, x.Inventory));
    }
}
