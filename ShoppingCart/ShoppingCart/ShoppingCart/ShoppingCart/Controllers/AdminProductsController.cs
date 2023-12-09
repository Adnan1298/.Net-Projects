using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Data;

namespace ShoppingCart.Controllers
{
    public class AdminProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        IWebHostEnvironment iw;

        public AdminProductsController(ApplicationDbContext context, IWebHostEnvironment i)
        {
            _context = context;
            iw = i;
        }
        [Authorize(Roles = "Admin")]
        // GET: AdminProducts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Products.Include(p => p.Category);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Order()
        {
            var query = from order in _context.Orders
                        join login in _context.Users on order.UsersId equals login.Id
                        join product in _context.Products on order.ProductId equals product.ProductId
                        select new OrderViewModel
                        {
                            OrderId = order.Order_Id,
                            Username = login.UserName,
                            Email = login.Email,
                            Address = order.Address,
                            OrderDate = order.DateTime,
                            CreditCardNo = order.CreditCard,
                            Cardholdername = order.Cardholdername,
                            ProductName = product.Name,
                            ProductPrice = Convert.ToDecimal(product.Price),
                            Qty = order.Quantity,
                            TotalAmount = order.TotalAmount
                        };
            var orders = query.ToList();
            return View(orders);

        }
        // GET: AdminProducts/Details/5
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

        // GET: AdminProducts/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name"); ;
            var id = HttpContext.Session.GetString("id");
            ViewBag.id = id;
            return View();
        }

        // POST: AdminProducts/Create
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
                            string d = Path.Combine(iw.WebRootPath, "AdminImages");
                            var fname = Path.GetFileName(img.FileName);
                            string filepath = Path.Combine(d, fname);
                            using (var fs = new FileStream(filepath, FileMode.Create))
                            {
                                await img.CopyToAsync(fs);
                            }
                            product.Images = @"\AdminImages\" + fname;
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

        // GET: AdminProducts/Edit/5
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

        // POST: AdminProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Product product)
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

        // GET: AdminProducts/Delete/5
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

        // POST: AdminProducts/Delete/5
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
        public async Task<IActionResult> Deletes(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var o = await _context.Orders
                .FirstOrDefaultAsync(m => m.Order_Id == id);
            if (o == null)
            {
                return NotFound();
            }

            return View(o);
        }

        // POST: Cat/Delete/5
        [HttpPost, ActionName("Deletes")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletesConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'DbScart.Categories'  is null.");
            }
            var o = await _context.Orders.FindAsync(id);
            if (o != null)
            {
                _context.Orders.Remove(o);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Order));
        }
        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
