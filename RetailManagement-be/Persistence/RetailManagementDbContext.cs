using Microsoft.EntityFrameworkCore;
using RetailManagement_be.Models.Entities;


namespace RetailManagement_be.Persistence;

public class RetailManagementDbContext : DbContext
{
    public RetailManagementDbContext(DbContextOptions<RetailManagementDbContext> options) : base(options) { }
    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<PurchaseProduct> PurchaseProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PurchaseProduct>()
            .HasOne(pp => pp.Purchase)
            .WithMany(p => p.PurchaseProducts)
            .HasForeignKey(pp => pp.PurchaseId);

        modelBuilder.Entity<PurchaseProduct>()
            .HasOne(pp => pp.Product)
            .WithMany(p => p.PurchaseProducts)
            .HasForeignKey(pp => pp.ProductId);

    }
}
