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
  public class ProductsController : Controller
  {
    private readonly InStockTrackerContext _db;

    public ProductsController(InStockTrackerContext db)
    {
      _db = db;
    }

    public ActionResult Index(string name)
    {
      var query = _db.Products.AsQueryable();
      
      if (name != null)
      {
        query = query.Where(entry => entry.Name.Contains(name));
      }

      query.Include(category => category.Categories)
        .ThenInclude(join => join.Category)
        .ToList();
      
      return View(query);
    }

    public ActionResult Create()
    {
      ViewBag.CategoryId = _db.Categories.ToList();
      return View();
    }

    [HttpPost]
    public ActionResult Create(Product product, int[] CategoryId)
    {
      _db.Products.Add(product);
      foreach(int id in CategoryId)
      {
        _db.CategoryProduct.Add(new CategoryProduct() {CategoryId = id, ProductId = product.ProductId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisProduct = _db.Products
        .Include(category => category.Categories)
        .ThenInclude(join => join.Category)
        .FirstOrDefault(product => product.ProductId == id);
      if (!string.IsNullOrEmpty(thisProduct.ImgTitle))
      {
        ViewBag.ImageDataUrl = RetrieveImage(id);
      }
      return View(thisProduct);
    }

    public ActionResult Edit(int id)
    {
      Product thisProduct = _db.Products.FirstOrDefault(product => product.ProductId == id);
      ViewBag.CategoryId = _db.Categories.ToList();
      return View(thisProduct);
    }


    [HttpPost]
    public ActionResult Edit(Product product, int[] CategoryId)
    {
      _db.Entry(product).State = EntityState.Modified;
      
      foreach(int id in CategoryId)
      {
        _db.CategoryProduct.Add(new CategoryProduct() {CategoryId = id, ProductId = product.ProductId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Product thisProduct = _db.Products.FirstOrDefault(product => product.ProductId == id);
      return View(thisProduct);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Product thisProduct = _db.Products.FirstOrDefault(product => product.ProductId == id);
      _db.Products.Remove(thisProduct);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddPhoto(int id)
    {
      Product thisProduct = _db.Products.FirstOrDefault(product => product.ProductId == id);
      return View(thisProduct);
    }

    [HttpPost]
    public IActionResult UploadPhoto(int ProductId)
    {
      Product thisProduct = _db.Products.FirstOrDefault(product => product.ProductId == ProductId);
      thisProduct.ImgTitle = "FileName";
      foreach(var file in Request.Form.Files)
      {
        MemoryStream ms = new MemoryStream();
        file.CopyTo(ms);
        thisProduct.Img = ms.ToArray();
        ms.Close();
        ms.Dispose();
        _db.SaveChanges();
      }

      return RedirectToAction("Details", new { id = ProductId });
    }

    private string RetrieveImage(int id)
    {
      Product thisProduct = _db.Products.FirstOrDefault(product => product.ProductId == id);
      string imageBase64Data = Convert.ToBase64String(thisProduct.Img);
      string imageDataURL = string.Format("data:image/jpg;base64, {0}", imageBase64Data);
      return imageDataURL;
    }

    public ActionResult Search()
    {
      return View();
    }

    private IEnumerable<Product> Search(string name)
    {
      Regex searchphrase = new Regex(name, RegexOptions.IgnoreCase);
      IEnumerable<Product> products = _db.Products
        .Where(p => searchphrase.IsMatch(p.Name) || searchphrase.IsMatch(p.Manufacturer) || searchphrase.IsMatch(p.Description))
        .ToList();
      return products;
    }

    private IEnumerable<Product> SuperSearch(string name, string manufacturer, string description, string minPrice, string maxPrice)
    {
      IQueryable<Product> productQuery = _db.Products;
      if (!string.IsNullOrEmpty(name))
      {
        Regex searchphrase = new Regex(name, RegexOptions.IgnoreCase);
        productQuery = productQuery.Where(p => searchphrase.IsMatch(p.Name));
      }
      if (!string.IsNullOrEmpty(manufacturer))
      {
        Regex searchphrase = new Regex(manufacturer, RegexOptions.IgnoreCase);
        productQuery = productQuery.Where(p => searchphrase.IsMatch(p.Manufacturer));
      }
      if (!string.IsNullOrEmpty(description))
      {
        Regex searchphrase = new Regex(description, RegexOptions.IgnoreCase);
        productQuery = productQuery.Where(p => searchphrase.IsMatch(p.Description));
      }
      if (!string.IsNullOrEmpty(minPrice))
      {
        productQuery = productQuery.Where(p => Product.ConvertPrice(p.Price) > Product.ConvertPrice(minPrice));
      }
      if (!string.IsNullOrEmpty(maxPrice))
      {
        productQuery = productQuery.Where(p => Product.ConvertPrice(p.Price) < Product.ConvertPrice(maxPrice));
      }
      IEnumerable<Product> products = productQuery
        .Include(p => p.Categories)
        .ThenInclude(join => join.Category)
        .ToList();
      return products;
    }

    [HttpPost]
    public ActionResult Results(string name, string manufacturer, string description, string minPrice, string maxPrice)
    {
      if (string.IsNullOrEmpty(name))
      {
        return RedirectToAction("Index");
      }
      IEnumerable<Product> products;
      if (string.IsNullOrEmpty(manufacturer) && string.IsNullOrEmpty(description) && string.IsNullOrEmpty(minPrice) && string.IsNullOrEmpty(maxPrice))
      {
        products = Search(name);
      }
      else
      {
        products = SuperSearch(name, manufacturer, description, minPrice, maxPrice);
      }
      return View(products);
    }
  }
}