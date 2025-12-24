using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FuelStation.PartExchange.Domain.Interfaces;
using FuelStation.PartExchange.Infrastructure.Data;
using FuelStation.PartExchange.Infrastructure.Repositories;
using FuelStation.PartExchange.Infrastructure.Services;

namespace FuelStation.PartExchange.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var conn = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<PartExchangeDbContext>(opt =>
            opt.UseNpgsql(conn));

        services.AddScoped<IFuelStationRepository, FuelStationRepository>();
        services.AddScoped<IPartInventoryRepository, PartInventoryRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IUnitOfWork, InfrastructureUnitOfWork>();
        services.AddScoped<IPartMatchingService, PartMatchingService>();
        services.AddScoped<DataSeeder>();

        return services;
    }
}
