using Microsoft.EntityFrameworkCore;
using supplier_service.Models;

namespace supplier_service.Data;

public class SupplierDbContext : DbContext
{
    public SupplierDbContext(DbContextOptions<SupplierDbContext> options) : base(options) {}

    public DbSet<Supplier> Suppliers { get; set; }

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
    }
}
