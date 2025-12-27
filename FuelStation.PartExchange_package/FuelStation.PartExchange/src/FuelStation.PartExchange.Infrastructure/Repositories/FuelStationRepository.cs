using FuelStation.PartExchange.Domain.Interfaces;
using FuelStation.PartExchange.Infrastructure.Context;

namespace FuelStation.PartExchange.Infrastructure.Repositories;

public class FuelStationRepository(ApplicationContext db) : IFuelStationRepository
{
    public async Task AddAsync(Domain.Models.FuelStation station)
    {
        await db.FuelStations.AddAsync(station);
    }

    public Task<Domain.Models.FuelStation> GetByIdAsync(Guid id)
    {
        var getFuelStationById = db.FuelStations.FirstOrDefault(x => x.Id == id);
        if (getFuelStationById == null)
        {
            return null;
        }
        return Task.FromResult(getFuelStationById);
    }

   
    public Task<IEnumerable<Domain.Models.FuelStation>> GetByCityAsync(string city) =>
        Task.FromResult<IEnumerable<Domain.Models.FuelStation>>(db.FuelStations.Where(x => x.City == city).AsEnumerable());
}
