using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InStockTracker.Models
{
  public class InStockTrackerContext : DbContext
  {
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CategoryProduct> CategoryProduct { get; set; }

    public InStockTrackerContext(DbContextOptions options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder builder)
      {
        builder.Entity<Product>()
        .HasData(
          new Product { ProductId = 1, Name = "Webcam", Manufacturer = "Logitech", Description = "Logitech webcam", Stock = 10 },
          new Product { ProductId = 2, Name = "Keyboard", Manufacturer = "Razer", Description = "Razer mechanical keyboard", Stock = 5 },
          new Product { ProductId = 3, Name = "Mouse", Manufacturer = "Razer", Description = "Razer gaming mouse", Stock = 6 },
          new Product { ProductId = 4, Name = "Microphone", Manufacturer = "Blue", Description = "Blue snowball microphone", Stock = 11 }
        );

        builder.Entity<Category>()
        .HasData(
          new Category { CategoryId = 1, Title = "Electronics" }
        );

        builder.Entity<CategoryProduct>()
        .HasData(
          new CategoryProduct { CategoryProductId = 1, ProductId = 1, CategoryId = 1 },
          new CategoryProduct { CategoryProductId = 2, ProductId = 2, CategoryId = 1 },
          new CategoryProduct { CategoryProductId = 3, ProductId = 3, CategoryId = 1 },
          new CategoryProduct { CategoryProductId = 4, ProductId = 4, CategoryId = 1 }
        );
      }
  }
}