using System.Linq.Expressions;
using System.Transactions;

namespace FuelStation.PartExchange.Domain.Interfaces;

public interface IRepository<TEntity>
{
    Task<bool> AddEntity(TEntity entity);
    Task<List<T>> FromSqlQuery<T>(FormattableString sql);
    Task ExecureSqlQuery(FormattableString sql);

    IQueryable<TEntity> GetEntitiesByQuery(bool isNotTracking = true,
        params Expression<Func<TEntity, object>>[] includes);

    IQueryable<TEntity> GetEntitiesByQueryIgnoreFilter(Expression<Func<TEntity, bool>> predicate, bool isNotTracking = true,
        params Expression<Func<TEntity, object>>[] includes);

    Task<TEntity?> GetEntitiesById(Guid id);
    void RemoveEntity(TEntity entity);
    Task RemoveEntity(Guid entityId);
    Task HardRemoveEntity(TEntity entity);
    Task SaveChanges();
    Task UpdateEntity(TEntity entity);
    Task CloseConnection();
    Task OpenConnection();
    void EnlistTransaction(Transaction transaction);
}