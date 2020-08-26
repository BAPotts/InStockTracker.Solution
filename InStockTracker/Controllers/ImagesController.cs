using InStockTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InStockTracker.Controllers
{
  public class ImagesController : Controller
  {
    private readonly InStockTrackerContext _db;

    public ImagesController(InStockTrackerContext db)
    {
      _db = db;
    }

    [HttpPost]
    public IActionResult UploadPhoto(int ProductId)
    {
      Product thisProduct = _db.Products.FirstOrDefault(product => product.ProductId == ProductId);
      foreach(var file in Request.Form.Files)
      {
        MemoryStream ms = new MemoryStream();
        file.CopyTo(ms);
        Image newImage = new Image() { Img = ms.ToArray(), ImgTitle = "FileName"};
        thisProduct.Images.Add(newImage);
        ms.Close();
        ms.Dispose();
        _db.SaveChanges();
      }

      return RedirectToAction("Details", "Products", new { id = ProductId });
    }
  }
}