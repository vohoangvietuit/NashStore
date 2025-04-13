using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Product>()
               .HasOne(p => p.Category)
               .WithMany(c => c.Products)
               .HasForeignKey(p => p.CategoryId)
               .OnDelete(DeleteBehavior.Cascade); // Delete products when a category is deleted

        builder.Entity<Order>()
               .HasMany(o => o.OrderDetails)
               .WithOne(od => od.Order)
               .HasForeignKey(od => od.OrderId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<OrderDetail>()
               .HasOne(od => od.Product)
               .WithMany()
               .HasForeignKey(od => od.ProductId);

        // Seed data
        builder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Electronics", Brand = "Various" },
            new Category { Id = 2, Name = "Clothing", Brand = "Generic" }
        );
    }
}
