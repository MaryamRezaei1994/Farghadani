using FuelStation.PartExchange.Domain.Interfaces;
using FuelStation.PartExchange.Domain.Models;

namespace FuelStation.PartExchange.Application.Services;

public class PartMatchingService : IPartMatchingService
{
    public Task<List<(Domain.Models.FuelStation Station, StationInventory Inventory)>> FindPartInCityAsync
        (Guid requestingStationId, int quantity, string partNumber)
    {
        throw new NotImplementedException();
    }
}