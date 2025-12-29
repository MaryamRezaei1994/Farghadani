using System.Linq.Expressions;
using System.Transactions;
using FuelStation.PartExchange.Domain.Interfaces;
using FuelStation.PartExchange.Domain.Models;

namespace FuelStation.PartExchange.Infrastructure.Repositories;

public class StationInventoryRepository : IRepository<StationInventory>
{
    public Task<bool> AddEntity(StationInventory entity)
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

    public IQueryable<StationInventory> GetEntitiesByQuery(bool isNotTracking = true, params Expression<Func<StationInventory, object>>[] includes)
    {
        throw new NotImplementedException();
    }

    public IQueryable<StationInventory> GetEntitiesByQueryIgnoreFilter(Expression<Func<StationInventory, bool>> predicate, bool isNotTracking = true,
        params Expression<Func<StationInventory, object>>[] includes)
    {
        throw new NotImplementedException();
    }

    public Task<StationInventory?> GetEntitiesById(Guid id)
    {
        throw new NotImplementedException();
    }

    public void RemoveEntity(StationInventory entity)
    {
        throw new NotImplementedException();
    }

    public Task RemoveEntity(Guid entityId)
    {
        throw new NotImplementedException();
    }

    public Task HardRemoveEntity(StationInventory entity)
    {
        throw new NotImplementedException();
    }

    public Task SaveChanges()
    {
        throw new NotImplementedException();
    }

    public Task UpdateEntity(StationInventory entity)
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