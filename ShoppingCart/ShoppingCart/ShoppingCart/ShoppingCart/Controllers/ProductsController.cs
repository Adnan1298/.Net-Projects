using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shopping.Data;
using ShoppingCart.Data;

namespace ShoppingCart.Controllers
{
    public class ProductsController : Controller
    {

        private readonly ApplicationDbContext _context;
        IWebHostEnvironment iw;
        private readonly IHttpContextAccessor ca;


        public ProductsController(ApplicationDbContext context, IWebHostEnvironment i, IHttpContextAccessor acs)
        {
            _context = context;
            iw = i;
            ca = acs;
        }
        [Authorize(Roles = "Vendor")]
        public IActionResult Index()
        {
            string email = HttpContext.Session.GetString("id");
            var query = from productss in _context.Products
                        join login in _context.Users on productss.UserId equals login.Id
                        join product in _context.Products on productss.ProductId equals product.ProductId
                        where login.Id == email
                        select new Product
                        {
                            ProductId = product.ProductId,
                            Name = product.Name,
                            Description = product.Description,
                            LDescription = product.LDescription,
                            Price = product.Price,
                            Brand = product.Brand,
                            Slug = product.Slug,
                            Category = product.Category,
                            CategoryId = product.CategoryId,
                            Images = product.Images,
                        };
            var orders = query.ToList();
            return View(orders);
        }
        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name");
            var id = HttpContext.Session.GetString("id");
            ViewBag.id = id;
            return View();
        }

        // POST: Plants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile img)
        {
            if (img != null)
            {
                string ext = Path.GetExtension(img.FileName);
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".gif" || ext == ".png")
                {
                    switch (ext)
                    {
                        case ".jpg":
                        case ".jpeg":
                        case ".gif":
                        case ".png":
                            string d = Path.Combine(iw.WebRootPath, "Image");
                            var fname = Path.GetFileName(img.FileName);
                            string filepath = Path.Combine(d, fname);
                            using (var fs = new FileStream(filepath, FileMode.Create))
                            {
                                await img.CopyToAsync(fs);
                            }
                            product.Images = @"\Image\" + fname;
                            //ca.HttpContext.Session.SetString("ids", product.UserId);
                            _context.Add(product);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                            break;
                        default:
                            ViewBag.i = "Wrong Image Format";
                            break;
                    }
                }
                else
                {
                    ViewBag.m = "must add picture";
                }
            }
            return View(product);
        }

        // GET: Products1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Products1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
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
            if (_context.Products == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
