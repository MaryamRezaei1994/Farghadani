using System.Linq.Expressions;
using System.Transactions;
using FuelStation.PartExchange.Domain.Interfaces;
using FuelStation.PartExchange.Domain.Models;

namespace FuelStation.PartExchange.Infrastructure.Repositories;

public class PartRepository : IRepository<Part>
{
    public Task<bool> AddEntity(Part entity)
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

    public IQueryable<Part> GetEntitiesByQuery(bool isNotTracking = true, params Expression<Func<Part, object>>[] includes)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Part> GetEntitiesByQueryIgnoreFilter(Expression<Func<Part, bool>> predicate, bool isNotTracking = true,
        params Expression<Func<Part, object>>[] includes)
    {
        throw new NotImplementedException();
    }

    public Task<Part?> GetEntitiesById(Guid id)
    {
        throw new NotImplementedException();
    }

    public void RemoveEntity(Part entity)
    {
        throw new NotImplementedException();
    }

    public Task RemoveEntity(Guid entityId)
    {
        throw new NotImplementedException();
    }

    public Task HardRemoveEntity(Part entity)
    {
        throw new NotImplementedException();
    }

    public Task SaveChanges()
    {
        throw new NotImplementedException();
    }

    public Task UpdateEntity(Part entity)
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