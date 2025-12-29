using System.Linq.Expressions;
using System.Transactions;
using FuelStation.PartExchange.Domain.Interfaces;
using FuelStation.PartExchange.Infrastructure.Context;

namespace FuelStation.PartExchange.Infrastructure.Repositories;

public class FuelStationRepository(ApplicationContext db) : IFuelStationRepository, IRepository<Domain.Models.FuelStation>
{
    public async Task AddAsync(Domain.Models.FuelStation station)
    {
        await db.FuelStations.AddAsync(station);
    }

    public Task<Domain.Models.FuelStation> GetByIdAsync(Guid id)
    {
        var getFuelStationById = db.FuelStations.FirstOrDefault(x => x.Id == id);
        if (getFuelStationById == null)
        {
            return null;
        }
        return Task.FromResult(getFuelStationById);
    }

   
    public Task<IEnumerable<Domain.Models.FuelStation>> GetByCityAsync(string city) =>
        Task.FromResult<IEnumerable<Domain.Models.FuelStation>>(db.FuelStations.Where(x => x.City == city).AsEnumerable());

    public Task<bool> AddEntity(Domain.Models.FuelStation entity)
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

    public IQueryable<Domain.Models.FuelStation> GetEntitiesByQuery(bool isNotTracking = true, params Expression<Func<Domain.Models.FuelStation, object>>[] includes)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Domain.Models.FuelStation> GetEntitiesByQueryIgnoreFilter(Expression<Func<Domain.Models.FuelStation, bool>> predicate, bool isNotTracking = true,
        params Expression<Func<Domain.Models.FuelStation, object>>[] includes)
    {
        throw new NotImplementedException();
    }

    public Task<Domain.Models.FuelStation?> GetEntitiesById(Guid id)
    {
        throw new NotImplementedException();
    }

    public void RemoveEntity(Domain.Models.FuelStation entity)
    {
        throw new NotImplementedException();
    }

    public Task RemoveEntity(Guid entityId)
    {
        throw new NotImplementedException();
    }

    public Task HardRemoveEntity(Domain.Models.FuelStation entity)
    {
        throw new NotImplementedException();
    }

    public Task SaveChanges()
    {
        throw new NotImplementedException();
    }

    public Task UpdateEntity(Domain.Models.FuelStation entity)
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
