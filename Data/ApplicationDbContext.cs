using IntegratedInventoryAndOrderManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IntegratedInventoryAndOrderManagementSystem.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
    public DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<InventoryAdjustment> InventoryAdjustments { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Shipment> Shipments { get; set; }
    public DbSet<SalesOrder> SalesOrders { get; set; }
    public DbSet<SalesOrderItem> SalesOrderItems { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<Receiving> Receivings { get; set; }

    // protected override void OnModelCreating(ModelBuilder builder)
    // {
    //     base.OnModelCreating(builder);

    //     var admin = new IdentityRole("admin");
    //     admin.NormalizedName = "admin";

    //     builder.Entity<IdentityRole>().HasData(admin);
    // }

}
