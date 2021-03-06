using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InStockTracker.Models
{
  public class Product
  {
    public Product()
    {
      this.Categories = new HashSet<CategoryProduct>();
      this.Images = new List<Image>();
    }
    public int ProductId { get; set; }
    [Required(ErrorMessage = "Product name is required")]
    [StringLength(255)]
    public string Name { get; set; }
    [StringLength(255)]
    public string Manufacturer { get; set; }
    [MaxLength(1000)]
    public string Description { get; set; }
    [Required(ErrorMessage = "Price is required")]
    public decimal Price { get; set; }
    [Required(ErrorMessage = "Stock is required")]
    [Range(1, 1000, ErrorMessage = "Stock must be between 1 and 1000")]
    public int Stock { get; set; }
    public List<Image> Images { get; set; }
    public virtual ICollection<CategoryProduct> Categories { get; set; }

    public static decimal ConvertPrice(string price)
    {
      if (price.Substring(0, 1) == "$")
      {
        return decimal.Parse(price.Substring(1));
      }
      return decimal.Parse(price);
    }
  }
}