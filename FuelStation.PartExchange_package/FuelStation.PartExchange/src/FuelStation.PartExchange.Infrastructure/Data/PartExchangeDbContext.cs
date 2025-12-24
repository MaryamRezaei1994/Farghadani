using Microsoft.EntityFrameworkCore;
using FuelStation.PartExchange.Domain.Entities;

namespace FuelStation.PartExchange.Infrastructure.Data;

public class PartExchangeDbContext : DbContext
{
    public PartExchangeDbContext(DbContextOptions<PartExchangeDbContext> options) : base(options)
    {
    }

    public DbSet<FuelStation> FuelStations { get; set; } = null!;
    public DbSet<Part> Parts { get; set; } = null!;
    public DbSet<StationInventory> StationInventories { get; set; } = null!;
    public DbSet<PartRequest> PartRequests { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Invoice> Invoices { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<FuelStation>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Name).HasMaxLength(200).IsRequired();
            b.Property(x => x.City).HasMaxLength(100).IsRequired();
            b.Property(x => x.Address).HasMaxLength(500);
        });

        modelBuilder.Entity<Part>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.PartNumber).IsRequired();
            b.HasIndex(x => x.PartNumber).IsUnique();
        });

        modelBuilder.Entity<StationInventory>(b =>
        {
            b.HasKey(x => new { x.StationId, x.PartId });
        });

        modelBuilder.Entity<PartRequest>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.PartNumber).IsRequired();
            // Set default CreatedAt to current time in database (Postgres: NOW())
            b.Property(x => x.CreatedAt).HasDefaultValueSql("NOW()");
        });

        modelBuilder.Entity<Order>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.CreatedAt).HasDefaultValueSql("NOW()");
        });

        modelBuilder.Entity<Invoice>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.IssuedAt).HasDefaultValueSql("NOW()");
        });
    }
}
