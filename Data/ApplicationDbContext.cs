using Microsoft.EntityFrameworkCore;
using inventorybackend.Api.Models;

namespace inventorybackend.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseItem> PurchaseItems { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure relationships and constraints
            modelBuilder.Entity<InventoryItem>()
                .HasOne(i => i.Supplier)
                .WithMany(s => s.InventoryItems)
                .HasForeignKey(i => i.SupplierId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<Purchase>()
                .HasOne(p => p.Supplier)
                .WithMany(s => s.Purchases)
                .HasForeignKey(p => p.SupplierId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.InventoryItem)
                .WithMany(ii => ii.OrderItems)
                .HasForeignKey(oi => oi.InventoryItemId);

            modelBuilder.Entity<PurchaseItem>()
                .HasOne(pi => pi.Purchase)
                .WithMany(p => p.PurchaseItems)
                .HasForeignKey(pi => pi.PurchaseId);

            modelBuilder.Entity<PurchaseItem>()
                .HasOne(pi => pi.InventoryItem)
                .WithMany()
                .HasForeignKey(pi => pi.InventoryItemId);

            modelBuilder.Entity<User>()
                .HasData(new User { Id = 1, Username = "admin", Email = "admin@example.com" });
        }


    }

}