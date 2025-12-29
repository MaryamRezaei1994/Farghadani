using System.Linq.Expressions;
using System.Transactions;
using FuelStation.PartExchange.Domain.Interfaces;
using FuelStation.PartExchange.Domain.Models;

namespace FuelStation.PartExchange.Infrastructure.Repositories;

public class InvoiceRepository : IRepository<Invoice>
{
    public Task<bool> AddEntity(Invoice entity)
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

    public IQueryable<Invoice> GetEntitiesByQuery(bool isNotTracking = true, params Expression<Func<Invoice, object>>[] includes)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Invoice> GetEntitiesByQueryIgnoreFilter(Expression<Func<Invoice, bool>> predicate, bool isNotTracking = true,
        params Expression<Func<Invoice, object>>[] includes)
    {
        throw new NotImplementedException();
    }

    public Task<Invoice?> GetEntitiesById(Guid id)
    {
        throw new NotImplementedException();
    }

    public void RemoveEntity(Invoice entity)
    {
        throw new NotImplementedException();
    }

    public Task RemoveEntity(Guid entityId)
    {
        throw new NotImplementedException();
    }

    public Task HardRemoveEntity(Invoice entity)
    {
        throw new NotImplementedException();
    }

    public Task SaveChanges()
    {
        throw new NotImplementedException();
    }

    public Task UpdateEntity(Invoice entity)
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