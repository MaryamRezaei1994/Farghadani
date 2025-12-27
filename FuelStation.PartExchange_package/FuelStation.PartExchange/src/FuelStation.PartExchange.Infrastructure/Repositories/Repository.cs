using System.Linq.Expressions;
using System.Transactions;
using EntityFramework.BulkInsert.Extensions;
using FuelStation.PartExchange.Domain.Interfaces;
using FuelStation.PartExchange.Domain.Models;
using FuelStation.PartExchange.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FuelStation.PartExchange.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseModel
{


    private readonly ApplicationContext _context;
    private readonly DbSet<TEntity> _dbSet;
    private readonly ILogger<Repository<TEntity>> _logger;


    public Repository(ApplicationContext context, ILogger<Repository<TEntity>> logger)
    {
        this._context = context;
        _logger = logger;
        this._dbSet = this._context.Set<TEntity>();

    }

    public IQueryable<TEntity> GetEntitiesByQuery(bool isNotTracking = true,
                   params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = null;
        if (isNotTracking is false)
            query = _dbSet.AsQueryable();
        else
            query = _dbSet.AsQueryable().AsNoTracking();


        includes.ToList().ForEach(include =>
        {
            if (include != null)
            {
                query = query.Include(include);
            }
        });

        return query;
    }
    public IQueryable<TEntity> GetEntitiesByQueryIgnoreFilter(bool isNotTracking = true,
               params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = null;
        if (isNotTracking is false)
            query = _dbSet.IgnoreQueryFilters().AsQueryable();
        else
            query = _dbSet.IgnoreQueryFilters().AsQueryable().AsNoTracking();


        includes.ToList().ForEach(include =>
        {
            if (include != null)
            {
                query = query.Include(include);
            }
        });

        return query;
    }
    public IQueryable<TEntity> GetEntitiesByQueryTracking(
    params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();

        includes.ToList().ForEach(include =>
        {
            if (include != null)
            {
                query = query.Include(include);
            }
        });

        return query;
    }

    public async Task<List<T>> FromSqlQuery<T>(FormattableString sql)
    {
        try
        {
            var result = await _context.Database.SqlQuery<T>(sql).ToListAsync();
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine("RawQuery Err:" + ex.ToString());
            throw;
        }
    }
    public async Task ExecureSqlQuery(FormattableString sql)
    {
        try
        {
        await _context.Database.ExecuteSqlAsync(sql);

        }
        catch (Exception ex)
        {
            Console.WriteLine("RawQuery Err:" + ex.ToString());
            throw;
        }
    }



    public async Task<TEntity?> GetEntitiesById(Guid id) => await _dbSet.FirstOrDefaultAsync(s => s.Id == id);

    public async Task<bool> AddEntity(TEntity entity)
    {
        try
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = entity.CreatedAt;
            entity.Id = Guid.NewGuid();
            await _dbSet.AddAsync(entity);
            if (entity.Id != Guid.Empty)
            {
                return true;
            }

            return false;
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return false;
        }
    }

    public async Task UpdateEntity(TEntity entity)
    {
        try
        {
            entity.UpdatedAt = DateTime.UtcNow; 
            _dbSet.Update(entity);
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
        }
    }

    public void RemoveEntity(TEntity entity)
    {
        try
        {
            entity.IsDeleted = true;
            _dbSet.Entry(entity).Property(x => x.IsDeleted).IsModified = true;
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
        }
    }

    public async Task RemoveEntity(Guid entityId)
    {
        try
        {
            var entity = await GetEntitiesById(entityId);
            if (entity is not null)
            {
                RemoveEntity(entity);
            }
        }

        catch (Exception e)
        {
            _logger.LogError(e.ToString());
        }
    }

    public async Task HardRemoveEntity(TEntity entity)
    {
        try
        {
            //var entity = await GetEntitiesById(entityId);
            _dbSet.Remove(entity);
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
        }
    }

    public async Task SaveChanges()
    {
        try
        {
            var res = await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    
    public async Task CloseConnection() => await _context.Database.CloseConnectionAsync();

    public async Task OpenConnection() => await _context.Database.OpenConnectionAsync();

    public void EnlistTransaction(Transaction transaction) => _context.Database.EnlistTransaction(transaction);
}