namespace InStockTracker.Models
{
  public class CategoryProduct
  {
    public int CategoryProductId { get; set; }
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    public Product Product { get; set; }
    public Category Category { get; set; }
  }
}