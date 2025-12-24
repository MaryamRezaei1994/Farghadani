using Microsoft.EntityFrameworkCore;
using FuelStation.PartExchange.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace FuelStation.PartExchange.Infrastructure.Data;

public class DataSeeder
{
    private readonly PartExchangeDbContext _db;
    private readonly ILogger<DataSeeder> _logger;

    public DataSeeder(PartExchangeDbContext db, ILogger<DataSeeder> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task SeedAsync()
    {
        await _db.Database.MigrateAsync();

        if (await _db.FuelStations.AnyAsync())
        {
            _logger.LogInformation("Database already seeded.");
            return;
        }

        _logger.LogInformation("Seeding initial data...");

        var s1 = new Domain.Entities.FuelStation { Id = Guid.NewGuid(), Name = "Station A", City = "Tehran", Address = "Address A" };
        var s2 = new Domain.Entities.FuelStation { Id = Guid.NewGuid(), Name = "Station B", City = "Tehran", Address = "Address B" };

        var p1 = new Part { Id = Guid.NewGuid(), PartNumber = "P-100", Name = "Fuel Pump" };
        var p2 = new Part { Id = Guid.NewGuid(), PartNumber = "P-200", Name = "Nozzle" };

        var inv1 = new StationInventory { StationId = s1.Id, PartId = p1.Id, Quantity = 0 };
        var inv2 = new StationInventory { StationId = s2.Id, PartId = p1.Id, Quantity = 10 };

        await _db.FuelStations.AddRangeAsync(s1, s2);
        await _db.Parts.AddRangeAsync(p1, p2);
        await _db.StationInventories.AddRangeAsync(inv1, inv2);

        await _db.SaveChangesAsync();

        _logger.LogInformation("Seeding complete.");
    }
}
