using FuelStation.PartExchange.Domain.Entities;

namespace FuelStation.PartExchange.Domain.Interfaces;

public interface IFuelStationRepository
{
    Task<Entities.FuelStation?> GetByIdAsync(Guid id);
    Task<IEnumerable<Entities.FuelStation>> GetByCityAsync(string city);
    Task AddAsync(Entities.FuelStation station);
}
