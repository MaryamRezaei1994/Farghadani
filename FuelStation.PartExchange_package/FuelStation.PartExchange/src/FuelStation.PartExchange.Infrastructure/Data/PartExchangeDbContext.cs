using Microsoft.EntityFrameworkCore;
using FuelStation.PartExchange.Domain.Entities;

namespace FuelStation.PartExchange.Infrastructure.Data;

public class PartExchangeDbContext : DbContext
{
    public PartExchangeDbContext(DbContextOptions<PartExchangeDbContext> options) : base(options) { }

    public DbSet<Domain.Entities.FuelStation> FuelStations { get; set; } = null!;
    public DbSet<Part> Parts { get; set; } = null!;
    public DbSet<StationInventory> StationInventories { get; set; } = null!;
    public DbSet<PartRequest> PartRequests { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Invoice> Invoices { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Domain.Entities.FuelStation>(eb =>
        {
            eb.HasKey(x => x.Id);
            eb.Property(x => x.Name).HasMaxLength(200).IsRequired();
            eb.Property(x => x.City).HasMaxLength(100).IsRequired();
            eb.Property(x => x.Address).HasMaxLength(500);
        });

        modelBuilder.Entity<Part>(eb =>
        {
            eb.HasKey(x => x.Id);
            eb.Property(x => x.PartNumber).IsRequired();
            eb.HasIndex(x => x.PartNumber).IsUnique();
        });

        modelBuilder.Entity<StationInventory>(eb =>
        {
            eb.HasKey(x => new { x.StationId, x.PartId });
        });
    }
}
