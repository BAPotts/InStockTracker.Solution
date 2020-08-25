using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InStockTracker.Models
{
  public class Category
  {
    public Category()
    {
      this.Products = new HashSet<CategoryProduct>();
    }
    public int CategoryId { get; set; }
    [Required(ErrorMessage = "Title is required")]
    [StringLength(255)]
    public string Title { get; set; }
    public virtual ICollection<CategoryProduct> Products { get; set; }
  }
}