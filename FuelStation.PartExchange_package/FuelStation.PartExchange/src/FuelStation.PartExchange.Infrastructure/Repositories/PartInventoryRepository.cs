using FuelStation.PartExchange.Domain.Entities;
using FuelStation.PartExchange.Domain.Interfaces;
using FuelStation.PartExchange.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FuelStation.PartExchange.Infrastructure.Repositories;

public class PartInventoryRepository : IPartInventoryRepository
{
    private readonly PartExchangeDbContext _db;

    public PartInventoryRepository(PartExchangeDbContext db) => _db = db;

    public async Task AddInventoryAsync(StationInventory inventory)
    {
        await _db.StationInventories.AddAsync(inventory);
    }

    public async Task DecreaseInventoryAsync(Guid stationId, Guid partId, int quantity)
    {
        var inv = await _db.StationInventories.FindAsync(new object[] { stationId, partId });
        if (inv == null) return;
        inv.Quantity -= quantity;
    }

    public async Task<StationInventory?> GetInventoryAsync(Guid stationId, string partNumber)
    {
        var part = await _db.Parts.FirstOrDefaultAsync(p => p.PartNumber == partNumber);
        if (part == null) return null;
        return await _db.StationInventories.FindAsync(new object[] { stationId, part.Id }) as StationInventory;
    }

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
