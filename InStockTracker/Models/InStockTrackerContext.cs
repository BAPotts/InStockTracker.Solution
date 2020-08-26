using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InStockTracker.Models
{
  public class InStockTrackerContext : IdentityDbContext<User>
  {
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CategoryProduct> CategoryProduct { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Image> Images { get; set; }

    public InStockTrackerContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
      builder.Entity<Product>()
      .HasData(
        new Product { ProductId = 1, Name = "Webcam", Manufacturer = "Logitech", Description = "Logitech webcam", Price = 99.99m, Stock = 10 },
        new Product { ProductId = 2, Name = "Keyboard", Manufacturer = "Razer", Description = "Razer mechanical keyboard", Price = 149.99m, Stock = 5 },
        new Product { ProductId = 3, Name = "Mouse", Manufacturer = "Razer", Description = "Razer gaming mouse", Price = 49.99m, Stock = 6 },
        new Product { ProductId = 4, Name = "Microphone", Manufacturer = "Blue", Description = "Blue snowball microphone", Price = 49.99m, Stock = 11 }
      );

      builder.Entity<Category>()
      .HasData(
        new Category { CategoryId = 1, Title = "Electronics" },
        new Category { CategoryId = 2, Title = "Appliances" },
        new Category { CategoryId = 3, Title = "Clothing" },
        new Category { CategoryId = 4, Title = "Books" },
        new Category { CategoryId = 5, Title = "Tools" },
        new Category { CategoryId = 6, Title = "Home" },
        new Category { CategoryId = 7, Title = "Garden" }
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