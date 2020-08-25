using Microsoft.AspNetCore.Mvc;
using InStockTracker.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
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
      return View();
    }

    [HttpPost]
    public ActionResult Create(Product product)
    {
      _db.Products.Add(product);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisProduct = _db.Products
        .Include(category => category.Categories)
        .ThenInclude(join => join.Category)
        .FirstOrDefault(product => product.ProductId == id);
      return View(thisProduct);
    }

    public ActionResult Edit(int id)
    {
      Product thisProduct = _db.Products.FirstOrDefault(product => product.ProductId == id);
      return View(thisProduct);
    }


    [HttpPost]
    public ActionResult Edit(Product product)
    {
      _db.Entry(product).State = EntityState.Modified;
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
  }
}