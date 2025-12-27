using FuelStation.PartExchange.Domain.Models;

namespace FuelStation.PartExchange.Domain.Interfaces;

public interface IOrderRepository
{
    Task AddAsync(Order order);
    Task<Order?> GetByIdAsync(Guid id);
    Task UpdateAsync(Order order);
}
