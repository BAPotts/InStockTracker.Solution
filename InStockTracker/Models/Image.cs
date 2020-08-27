using System;

namespace InStockTracker.Models
{
  public class Image
  {
    public int ImageId { get; set; }

    public byte[] Img { get; set; }

    public string ImgTitle { get; set; }

    public Product ParentProduct { get; set; }

    public static string RetrieveImage(Image image)
    {
      string imageBase64Data = Convert.ToBase64String(image.Img);
      string imageDataURL = string.Format("data:image/jpg;base64, {0}", imageBase64Data);
      return imageDataURL;
    }
  }
}