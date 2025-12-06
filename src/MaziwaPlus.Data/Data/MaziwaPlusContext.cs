using Microsoft.EntityFrameworkCore;
using MaziwaPlus.Domain.Entities;

namespace MaziwaPlus.Data.Data;

public class MaziwaPlusContext : DbContext
{
    public MaziwaPlusContext(DbContextOptions<MaziwaPlusContext> options) : base(options) { }

    public DbSet<Farmer> Farmers => Set<Farmer>();
    public DbSet<MilkCollection> MilkCollections => Set<MilkCollection>();
    public DbSet<Shop> Shops => Set<Shop>();
    public DbSet<Delivery> Deliveries => Set<Delivery>();
    public DbSet<Payment> Payments => Set<Payment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Farmer>(b =>
        {
            b.HasKey(f => f.Id);
            b.Property(f => f.Name).IsRequired().HasMaxLength(200);
        });

        modelBuilder.Entity<MilkCollection>(b =>
        {
            b.HasKey(c => c.Id);
            b.HasOne(c => c.Farmer).WithMany(f => f.Collections).HasForeignKey(c => c.FarmerId).OnDelete(DeleteBehavior.Cascade);
            b.Property(c => c.LitersCollected).HasColumnType("decimal(10,2)");
            b.Property(c => c.RatePerLiter).HasColumnType("decimal(10,2)");
        });

        modelBuilder.Entity<Shop>(b => { b.HasKey(s => s.Id); b.Property(s => s.ShopName).IsRequired().HasMaxLength(200); });

        modelBuilder.Entity<Delivery>(b =>
        {
            b.HasKey(d => d.Id);
            b.HasOne(d => d.Shop).WithMany(s => s.Deliveries).HasForeignKey(d => d.ShopId);
            b.Property(d => d.LitersDelivered).HasColumnType("decimal(10,2)");
        });

        modelBuilder.Entity<Payment>(b =>
        {
            b.HasKey(p => p.Id);
            b.HasOne(p => p.Delivery).WithMany(d => d.Payments).HasForeignKey(p => p.DeliveryId);
            b.Property(p => p.AmountPaid).HasColumnType("decimal(10,2)");
        });
    }
}
