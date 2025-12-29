using FuelStation.PartExchange.Application.Interfaces.General;
using FuelStation.PartExchange.Application.Services.General;
using FuelStation.PartExchange.Domain.Interfaces;
using FuelStation.PartExchange.Domain.Models;
using FuelStation.PartExchange.Infrastructure.Context;
using FuelStation.PartExchange.Infrastructure.Repositories;
using FuelStation.PartExchange.Infrastructure.Services; // برای PartMatchingService
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using APIService = FuelStation.PartExchange.Infrastructure.Services.APIService;
using FileService = FuelStation.PartExchange.Infrastructure.Services.FileService;

namespace FuelStation.PartExchange.Infrastructure.Dependency;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // DbContext
        var conn = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationContext>(opt => opt.UseNpgsql(conn));

        // ✅ تنظیمات MinIO
        services.Configure<MinIOOptions>(configuration.GetSection(MinIOOptions.MinIO));
        services.AddSingleton<MinIoClientProvider>();
        services.AddScoped<IFileService, FileService>();

        // Generic repository (اگر نیاز داری — وگرنه حذفش کن)
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        // ریپازیتوری‌های خاص
        services.AddScoped<IRepository<Order>, OrderRepository>();
        services.AddScoped<IPartInventoryRepository, PartInventoryRepository>(); // ✅ اینجا ثبت شده
        services.AddScoped<IFuelStationRepository, FuelStationRepository>();

        // سرویس‌های لایه Infrastructure
        services.AddScoped<IPartMatchingService, PartMatchingService>(); // ✅ PartMatchingService از اینجا می‌گیره
        services.AddScoped<IAPIService, APIService>();

        return services;
    }
}