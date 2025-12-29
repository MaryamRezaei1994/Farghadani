// using FuelStation.PartExchange.Application.Interfaces;
// using FuelStation.PartExchange.Application.Services;
// using FuelStation.PartExchange.Domain.Interfaces;
// using FuelStation.PartExchange.Domain.Models;
// using FuelStation.PartExchange.Infrastructure.Context;
// using FuelStation.PartExchange.Infrastructure.Repositories;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using PartMatchingService = FuelStation.PartExchange.Infrastructure.Services.PartMatchingService;
//
// namespace FuelStation.PartExchange.Infrastructure.Dependency;
//
// public static class DependencyInjection
// {
//     public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
//     {
//         var conn = configuration.GetConnectionString("DefaultConnection");
//         services.AddDbContext<ApplicationContext>(opt => opt.UseNpgsql(conn));
//
//         //repository register
//         services.AddScoped<IRepository<Order>, OrderRepository>();
//         services.AddScoped<IRepository<Part>, PartRepository>();
//         services.AddScoped<IRepository<StationInventory>, StationInventoryRepository>();
//         services.AddScoped<IRepository<Domain.Models.FuelStation>, FuelStationRepository>();
//         services.AddScoped<IRepository<Invoice>, InvoiceRepository>();
//         services.AddScoped<IRepository<PartRequest>, PartRequestRepository>();
//         services.AddScoped<IRepository<PartInventoryRepository>, PartInventoryRepository>();
//
//         //service register
//         services.AddScoped<IPartMatchingService, PartMatchingService>();
//         services.AddScoped<IOrderPartService, OrderPartService>();
//         services.AddScoped<ICacheProvider, CacheProvider>();
//
//         return services;
//     }
// }

using FuelStation.PartExchange.Application.Interfaces.General;
using FuelStation.PartExchange.Domain.Interfaces;
using FuelStation.PartExchange.Domain.Models;
using FuelStation.PartExchange.Infrastructure.Context;
using FuelStation.PartExchange.Infrastructure.Repositories;
using FuelStation.PartExchange.Infrastructure.Services; // برای PartMatchingService
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FuelStation.PartExchange.Infrastructure.Dependency;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // DbContext
        var conn = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationContext>(opt => opt.UseNpgsql(conn));
        
        // Generic repository (اگر نیاز داری — وگرنه حذفش کن)
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        // ریپازیتوری‌های خاص
        services.AddScoped<IRepository<Order>, OrderRepository>();
        services.AddScoped<IPartInventoryRepository, PartInventoryRepository>(); // ✅ اینجا ثبت شده
        services.AddScoped<IFuelStationRepository, FuelStationRepository>();
     
        // سرویس‌های لایه Infrastructure
        services.AddScoped<IPartMatchingService, PartMatchingService>(); // ✅ PartMatchingService از اینجا می‌گیره
        services.AddScoped<IAPIService, APIService>(); 
        services.AddScoped<IFileService, FileService>(); 
        return services;
    }
}