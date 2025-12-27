using FuelStation.PartExchange.Domain.Interfaces;
using FuelStation.PartExchange.Infrastructure.Context;

namespace FuelStation.PartExchange.Infrastructure;

public class InfrastructureUnitOfWork : IUnitOfWork
{
    private readonly ApplicationContext _db;
    public InfrastructureUnitOfWork(ApplicationContext db) => _db = db;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => _db.SaveChangesAsync(cancellationToken);
}
