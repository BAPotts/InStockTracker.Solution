using System.Collections.Generic;

namespace InStockTracker.Models
{
  public class Category
  {
    public Category()
    {
      this.Products = new HashSet<CategoryProduct>();
    }
    public int CategoryId { get; set; }
    public string Title { get; set; }
    public virtual ICollection<CategoryProduct> Products { get; set; }
  }
}