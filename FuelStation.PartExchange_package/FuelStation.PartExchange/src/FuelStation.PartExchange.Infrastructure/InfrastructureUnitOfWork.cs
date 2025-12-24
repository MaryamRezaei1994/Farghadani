using FuelStation.PartExchange.Domain.Interfaces;
using FuelStation.PartExchange.Infrastructure.Data;

namespace FuelStation.PartExchange.Infrastructure;

public class InfrastructureUnitOfWork : IUnitOfWork
{
    private readonly PartExchangeDbContext _db;
    public InfrastructureUnitOfWork(PartExchangeDbContext db) => _db = db;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => _db.SaveChangesAsync(cancellationToken);
}
