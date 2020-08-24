using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InStockTracker.Models
{
  public class Product
  {
    public Product()
    {
      this.Categories = new HashSet<CategoryProduct>();
    }
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string ImgTitle { get; set; }
    public byte[] Img { get; set; }
    public virtual ICollection<CategoryProduct> Categories { get; set; }
  }
}