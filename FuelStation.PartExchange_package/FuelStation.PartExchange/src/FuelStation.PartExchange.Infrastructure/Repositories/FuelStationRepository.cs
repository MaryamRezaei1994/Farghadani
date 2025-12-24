using FuelStation.PartExchange.Domain.Entities;
using FuelStation.PartExchange.Domain.Interfaces;
using FuelStation.PartExchange.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FuelStation.PartExchange.Infrastructure.Repositories;

public class FuelStationRepository : IFuelStationRepository
{
    private readonly PartExchangeDbContext _db;

    public FuelStationRepository(PartExchangeDbContext db) => _db = db;

    public async Task AddAsync(Domain.Entities.FuelStation station)
    {
        await _db.FuelStations.AddAsync(station);
    }

    public Task<Domain.Entities.FuelStation?> GetByIdAsync(Guid id) => _db.FuelStations.FirstOrDefaultAsync(x => x.Id == id);

    public Task<IEnumerable<Domain.Entities.FuelStation>> GetByCityAsync(string city) =>
        Task.FromResult<IEnumerable<Domain.Entities.FuelStation>>(_db.FuelStations.Where(x => x.City == city).AsEnumerable());
}
