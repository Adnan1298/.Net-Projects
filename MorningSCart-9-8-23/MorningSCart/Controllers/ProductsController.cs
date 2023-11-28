using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MorningSCart.Models;

namespace MorningSCart.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DbScart db;
        IWebHostEnvironment iw;

        public ProductsController(DbScart dbs,IWebHostEnvironment iwc)
        {
            db = dbs;
            iw = iwc;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var dbScart = db.Products.Include(p => p.Category);
            return View(await dbScart.ToListAsync());
        }

        // GET: Products/Details/5
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

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(db.Categories, "CategoryId", "CName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
            public async Task<IActionResult> Create(Plants product, IFormFile img)
            {
                if (img !=null)
                {
                    string ext = Path.GetExtension(img.FileName);
                switch(ext)
                {
                    case ".jpg":
                    case ".gif":               
                        string d = Path.Combine(iw.WebRootPath, "Image");
                        var fname = Path.GetFileName(img.FileName);
                        string filePath = Path.Combine(d, fname);
                        using (var fs = new FileStream(filePath, FileMode.Create))
                        {
                            await img.CopyToAsync(fs);
                        }
                        product.PImage=@"\Image\"+fname;
                        db.Add(product);
                        await db.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                        break;
                    default:
                   
                        ViewBag.m="Wrong Picture Format";
                        break;
                    }
                }
                else
            {
                ViewBag.m="Must add Picture";
               
            }

            return View();

            //  ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CName", product.CategoryId);
            // return View(product);
        }
        

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.Products == null)
            {
                return NotFound();
            }

            var product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(db.Categories, "CategoryId", "CName", product.CategoryId);
            return View(product);
        }
        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Plants product)
        {
            if (id != product.PlantsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(product);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.PlantsId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(db.Categories, "CategoryId", "CName", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.Products == null)
            {
                return Problem("Entity set 'DbScart.Products'  is null.");
            }
            var product = await db.Products.FindAsync(id);
            if (product != null)
            {
                db.Products.Remove(product);
            }
            
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (db.Products?.Any(e => e.PlantsId == id)).GetValueOrDefault();
        }
    }
}
