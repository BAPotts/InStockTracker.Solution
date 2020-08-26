namespace InStockTracker.Models
{
  public class Image
  {
    public int ImageId { get; set; }

    public byte[] Img { get; set; }

    public string ImgTitle { get; set; }

    public Product ParentProduct { get; set; }
  }
}