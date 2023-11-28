using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MorningSCart.Models;
using System.Diagnostics;

namespace MorningSCart.Controllers
{
    public class HomeController : Controller
    {
        DbScart db;
        public HomeController(DbScart sc)
        {
            db=sc;
        }
        //public IActionResult SearchItems(string searchText)
        //{
        //    var viewModel = new SearchViewModel();

        //    if (!string.IsNullOrEmpty(searchText))
        //    {
        //        viewModel.Products = db.Products
        //            .Where(p => p.ProductName.Contains(searchText))
        //            .ToList();
        //    }
        //    else
        //    {
        //        viewModel.Products = db.Products.ToList();
        //    }

        //    viewModel.SearchText = searchText;

        //    return View(viewModel);
        //}
        public IActionResult Search(string s)
        {
            var searchProducts = db.Products
                .Where(p => p.ProductName.Contains(s))
                .ToList();

            return PartialView("Index", searchProducts);
        }

        public async Task<IActionResult> Index(string categorySlug = "")
        {
            //if (!string.IsNullOrEmpty(searchItem))
            //{
            //    var viewModel = new SearchViewModel();

            //    if (!string.IsNullOrEmpty(searchItem))
            //    {
            //        viewModel.Products = db.Products
            //            .Where(p => p.ProductName.Contains(searchItem))
            //            .ToList();
            //    }
            //    else
            //    {
            //        viewModel.Products = db.Products.ToList();
            //    }

            //    viewModel.SearchText = searchItem;

            //    return View(viewModel);
            //}
            ViewBag.CategorySlug = categorySlug;
            if (categorySlug == "")
            {
                return View(await db.Products.OrderByDescending(p => p.PlantsId).ToListAsync());
            }
            else
            {
                Category category = await db.Categories.Where(c => c.slug == categorySlug).FirstOrDefaultAsync();
                if (category == null) return RedirectToAction("Index");
                var productsByCategory = db.Products.Where(p => p.CategoryId == category.CategoryId);
                return View(await productsByCategory.OrderByDescending(p => p.PlantsId).ToListAsync());
            }
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || db.Products == null)
            {
                return NotFound();
            }

            var product = await db.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.PlantsId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}