using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Data;
using ShoppingCart.Data;
using ShoppingCart.Models;
using System.Diagnostics;

namespace ShoppingCart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor ca;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> usermanager, IHttpContextAccessor acs, ApplicationDbContext context)
        {
            _logger = logger;
            _userManager = usermanager;
            ca = acs;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //For Fetching User Id
            var user = await _userManager.GetUserAsync(User); // Get the current user.
            if (user != null)
            {
                var userId = user.Id;
                var Email = user.Email;
                var Name = user.Name;
                ca.HttpContext.Session.SetString("id", userId);
                ca.HttpContext.Session.SetString("u", Email);
                ca.HttpContext.Session.SetString("f", Name);
                // You can store other user-related data in the session as well if needed.
            }
            return View(await _context.Products.ToListAsync());
        }
        public async Task<IActionResult> Product(string categorySlug = "")
        {
            ViewBag.CategorySlug = categorySlug;
            if (categorySlug == "")
            {
                return View(await _context.Products.OrderByDescending(p => p.ProductId).ToListAsync());
            }
            else
            {
                Category category = await _context.Category.Where(c => c.Slug == categorySlug).FirstOrDefaultAsync();
                if (category == null) return RedirectToAction("Index");
                var productsByCategory = _context.Products.Where(p => p.CategoryId == category.CategoryId);
                return View(await productsByCategory.OrderByDescending(p => p.ProductId).ToListAsync());
            }
        }
        public async Task<IActionResult> SingleProduct(int? id)
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}