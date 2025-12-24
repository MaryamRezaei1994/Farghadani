using FuelStation.PartExchange.Domain.Entities;
using FuelStation.PartExchange.Domain.Interfaces;
using FuelStation.PartExchange.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FuelStation.PartExchange.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly PartExchangeDbContext _db;

    public OrderRepository(PartExchangeDbContext db) => _db = db;

    public async Task AddAsync(Order order)
    {
        await _db.Orders.AddAsync(order);
    }

    public Task<Order?> GetByIdAsync(Guid id) => _db.Orders.FirstOrDefaultAsync(o => o.Id == id);

    public Task UpdateAsync(Order order)
    {
        _db.Orders.Update(order);
        return Task.CompletedTask;
    }
}
