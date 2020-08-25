using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace InStockTracker.Models
{
  public class InStockTrackerContextFactory : IDesignTimeDbContextFactory<InStockTrackerContext>
  {
    InStockTrackerContext IDesignTimeDbContextFactory<InStockTrackerContext>.CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

      var builder = new DbContextOptionsBuilder<InStockTrackerContext>();
      var connectionString = configuration.GetConnectionString("DefaultConnection");

      builder.UseMySql(connectionString);

      return new InStockTrackerContext(builder.Options);
    }
  }
}