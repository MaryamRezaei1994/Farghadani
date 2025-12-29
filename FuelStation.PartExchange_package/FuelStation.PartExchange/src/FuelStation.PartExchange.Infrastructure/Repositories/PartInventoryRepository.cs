using System.Linq.Expressions;
using System.Transactions;
using FuelStation.PartExchange.Domain.Interfaces;
using FuelStation.PartExchange.Domain.Models;
using FuelStation.PartExchange.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FuelStation.PartExchange.Infrastructure.Repositories;

/// <summary>
/// Repository for managing station inventory and parts.
/// </summary>
public class PartInventoryRepository : IPartInventoryRepository, IRepository<PartInventoryRepository>
{
    private readonly ApplicationContext _db;

    public PartInventoryRepository(ApplicationContext db) => _db = db;

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
    public async Task<List<(Domain.Models.FuelStation Station, StationInventory Inventory)>> FindPartInCityAsync(
        string city, string partNumber)
    {
        var part = await _db.Parts.FirstOrDefaultAsync(p => p.PartNumber == partNumber);
        if (part == null)
            return new List<(Domain.Models.FuelStation, StationInventory)>();

        var query = _db.FuelStations
            .Join(
                _db.StationInventories,
                station => station.Id,
                inventory => inventory.StationId,
                (station, inventory) => new { Station = station, Inventory = inventory }
            )
            .Where(x => x.Station.City == city
                        && x.Inventory.PartId == part.Id
                        && x.Inventory.Quantity > 0);

        // ابتدا نتیجه را از دیتابیس بگیر (ToListAsync)
        var anonymousList = await query.ToListAsync();

        // سپس در حافظه به Tuple تبدیل کن
        var result = anonymousList
            .Select(x => (Station: x.Station, Inventory: x.Inventory))
            .ToList();

        return result;
    }

    public Task<bool> AddEntity(PartInventoryRepository entity)
    {
        throw new NotImplementedException();
    }

    public Task<List<T>> FromSqlQuery<T>(FormattableString sql)
    {
        throw new NotImplementedException();
    }

    public Task ExecureSqlQuery(FormattableString sql)
    {
        throw new NotImplementedException();
    }

    public IQueryable<PartInventoryRepository> GetEntitiesByQuery(bool isNotTracking = true, params Expression<Func<PartInventoryRepository, object>>[] includes)
    {
        throw new NotImplementedException();
    }

    public IQueryable<PartInventoryRepository> GetEntitiesByQueryIgnoreFilter(Expression<Func<PartInventoryRepository, bool>> predicate, bool isNotTracking = true,
        params Expression<Func<PartInventoryRepository, object>>[] includes)
    {
        throw new NotImplementedException();
    }

    public Task<PartInventoryRepository?> GetEntitiesById(Guid id)
    {
        throw new NotImplementedException();
    }

    public void RemoveEntity(PartInventoryRepository entity)
    {
        throw new NotImplementedException();
    }

    public Task RemoveEntity(Guid entityId)
    {
        throw new NotImplementedException();
    }

    public Task HardRemoveEntity(PartInventoryRepository entity)
    {
        throw new NotImplementedException();
    }

    public Task SaveChanges()
    {
        throw new NotImplementedException();
    }

    public Task UpdateEntity(PartInventoryRepository entity)
    {
        throw new NotImplementedException();
    }

    public Task CloseConnection()
    {
        throw new NotImplementedException();
    }

    public Task OpenConnection()
    {
        throw new NotImplementedException();
    }

    public void EnlistTransaction(Transaction transaction)
    {
        throw new NotImplementedException();
    }
}