using Microsoft.EntityFrameworkCore;
using supplier_service.Models;

namespace supplier_service.Data;

public class SupplierDbContext : DbContext
{
    public SupplierDbContext(DbContextOptions<SupplierDbContext> options) : base(options)
    {
    }

    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Good> Goods { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.ToTable("suppliers");
            entity.HasKey(e => e.supplier_id);
            entity.Property(e => e.name).HasMaxLength(100).IsRequired();
            entity.Property(e => e.contact_person).HasMaxLength(100);
            entity.Property(e => e.email).HasMaxLength(100).IsRequired();
            entity.Property(e => e.phone).HasMaxLength(20);
            entity.Property(e => e.status).HasDefaultValue("active");
        });
        modelBuilder.Entity<Good>(entity =>
        {
            entity.ToTable("good");

            entity.HasKey(g => g.id);

            entity.Property(g => g.purchase_price).HasColumnType("decimal(10,2)");
            entity.Property(g => g.received_date).HasDefaultValueSql("CURRENT_DATE");

            entity.HasOne(g => g.Supplier)
                .WithMany(s => s.Goods)
                .HasForeignKey(g => g.supplier_id)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(g => g.Item)
                .WithMany(i => i.Goods)
                .HasForeignKey(g => g.item_id)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(g => g.Warehouse)
                .WithMany(w => w.Goods)
                .HasForeignKey(g => g.warehouse_id)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.ToTable("items");
            entity.HasKey(i => i.id);
            entity.HasIndex(i => i.article).IsUnique();
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.ToTable("warehouse");
            entity.HasKey(w => w.id);
            entity.Property(w => w.is_active).HasDefaultValue(true);
        });
    }
}