using Microsoft.AspNetCore.Mvc;
using InStockTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace InStockTracker.Controllers
{
  public class CategoriesController : Controller
  {
    private readonly InStockTrackerContext _db;

    public CategoriesController(InStockTrackerContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Category> model = _db.Categories.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Category category)
    {
      _db.Categories.Add(category);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisCategory = _db.Categories
        .Include(category => category.Products)
        .ThenInclude(join => join.Product)
        .ThenInclude(product => product.Images)
        .FirstOrDefault(category => category.CategoryId == id);
      List<string> productNames = new List<string>();
      Dictionary<string, string> productImages = new Dictionary<string, string>();
      foreach(var join in thisCategory.Products)
      {
        if (join.Product.Images.Any())
        {
          string ImageData = RetrieveImage(join.Product.Images[0].ImageId);
          productNames.Add(join.Product.Name);
          productImages.Add(join.Product.Name, ImageData);
        }
      }
      ViewBag.ProductNames = productNames;
      ViewBag.ImageDictionary = productImages;
      return View(thisCategory);
    }

    public ActionResult Edit(int id)
    {
      var thisCategory = _db.Categories.FirstOrDefault(category => category.CategoryId == id);
      return View(thisCategory);
    }

    [HttpPost]
    public ActionResult Edit(Category category)
    {
      _db.Entry(category).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisCategory = _db.Categories.FirstOrDefault(category => category.CategoryId == id);
      return View(thisCategory);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisCategory = _db.Categories.FirstOrDefault(category => category.CategoryId == id);
      _db.Categories.Remove(thisCategory);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    private string RetrieveImage(int ImageId)
    {
      Image image = _db.Images.First(images => images.ImageId == ImageId);
      string imageBase64Data = Convert.ToBase64String(image.Img);
      string imageDataURL = string.Format("data:image/jpg;base64, {0}", imageBase64Data);
      return imageDataURL;
    }
  }
}