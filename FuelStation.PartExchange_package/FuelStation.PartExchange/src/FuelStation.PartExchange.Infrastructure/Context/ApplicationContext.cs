using System.Text.Json;
using FuelStation.PartExchange.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace FuelStation.PartExchange.Infrastructure.Context;

public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
{
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Domain.Models.FuelStation> FuelStations { get; set; } = null!;
    public DbSet<Part> Parts { get; set; } = null!;
    public DbSet<StationInventory> StationInventories { get; set; } = null!;
    public DbSet<PartRequest> PartRequest { get; set; } = null!;
    public DbSet<Invoice> Invoice { get; set; } = null!;
    public DbSet<EmbeddedMapping> EmbeddedMapping { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("FuelStation");

        modelBuilder.Entity<Order>().HasQueryFilter(x => x.IsDeleted == false);
        
        modelBuilder.Entity<Domain.Models.FuelStation>(b =>
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

        modelBuilder.Entity<Invoice>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.IssuedAt).HasDefaultValueSql("NOW()");
        });
        modelBuilder.Entity<EmbeddedMapping>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Key).IsUnique();

            entity.Property(e => e.Data)
                .HasColumnType("jsonb")
                .HasConversion(
                    v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
                    v => JsonSerializer.Deserialize<Dictionary<string, object>>(v, JsonSerializerOptions.Default)
                         ?? new Dictionary<string, object>()
                );
        });
    }
}