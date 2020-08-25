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
    [Required(ErrorMessage = "Product name is required")]
    [StringLength(255)]
    public string Name { get; set; }
    [StringLength(255)]
    public string Manufacturer { get; set; }
    [MaxLength(1000)]
    public string Description { get; set; }
    [Required(ErrorMessage = "Price is required")]
    [Range(1, 10000.00, ErrorMessage = "Price must be between $1.00 and $10,000.00")]
    public decimal Price { get; set; }
    [Required(ErrorMessage = "Stock is required")]
    [Range(1, 1000, ErrorMessage = "Stock must be between 1 and 1000")]
    public int Stock { get; set; }
    public string ImgTitle { get; set; }
    public byte[] Img { get; set; }
    public virtual ICollection<CategoryProduct> Categories { get; set; }
  }
}