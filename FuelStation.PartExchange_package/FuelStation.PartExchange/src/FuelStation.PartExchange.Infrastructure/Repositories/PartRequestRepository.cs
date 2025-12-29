using System.Linq.Expressions;
using System.Transactions;
using FuelStation.PartExchange.Domain.Interfaces;
using FuelStation.PartExchange.Domain.Models;

namespace FuelStation.PartExchange.Infrastructure.Repositories;

public class PartRequestRepository : IRepository<PartRequest>
{
    public Task<bool> AddEntity(PartRequest entity)
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

    public IQueryable<PartRequest> GetEntitiesByQuery(bool isNotTracking = true, params Expression<Func<PartRequest, object>>[] includes)
    {
        throw new NotImplementedException();
    }

    public IQueryable<PartRequest> GetEntitiesByQueryIgnoreFilter(Expression<Func<PartRequest, bool>> predicate, bool isNotTracking = true,
        params Expression<Func<PartRequest, object>>[] includes)
    {
        throw new NotImplementedException();
    }

    public Task<PartRequest?> GetEntitiesById(Guid id)
    {
        throw new NotImplementedException();
    }

    public void RemoveEntity(PartRequest entity)
    {
        throw new NotImplementedException();
    }

    public Task RemoveEntity(Guid entityId)
    {
        throw new NotImplementedException();
    }

    public Task HardRemoveEntity(PartRequest entity)
    {
        throw new NotImplementedException();
    }

    public Task SaveChanges()
    {
        throw new NotImplementedException();
    }

    public Task UpdateEntity(PartRequest entity)
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