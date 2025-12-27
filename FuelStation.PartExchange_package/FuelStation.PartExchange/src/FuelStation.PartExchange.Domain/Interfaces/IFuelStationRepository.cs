namespace FuelStation.PartExchange.Domain.Interfaces;

public interface IFuelStationRepository
{
    Task<Models.FuelStation> GetByIdAsync(Guid id);
    Task<IEnumerable<Models.FuelStation>> GetByCityAsync(string city);
    Task AddAsync(Models.FuelStation station);
}
