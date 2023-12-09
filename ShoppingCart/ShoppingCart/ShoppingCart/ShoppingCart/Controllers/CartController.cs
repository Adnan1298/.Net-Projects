using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Data;
using ShoppingCart.Models;
using System.Numerics;

namespace ShoppingCart.Controllers
{
    public class CartController : Controller
    {
        ApplicationDbContext sc;
        IHttpContextAccessor ca;
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext dbc, IHttpContextAccessor acs, ApplicationDbContext context)
        {
            sc = dbc;
            ca = acs;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            CartViewModel cartVM = new()
            {
                //pid=cart.(a=>a.ProductId),
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.Quantity * x.Price)
            };
            return View(cartVM);
        }

        public async Task<IActionResult> Add(int id)
        {
            ca.HttpContext.Session.SetInt32("pi", id);
            Product product = await sc.Products.FindAsync(id);

            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var pro = _context.Products.Where(x => x.ProductId == id).FirstOrDefault();

            CartItem cartItem = cart.Where(c => c.ProductsId == id).FirstOrDefault();

            if (cartItem == null)
            {
                cart.Add(new CartItem(product));
                ca.HttpContext.Session.SetInt32("q", 1);
                ca.HttpContext.Session.SetString("ids", pro.UserId );
            }
            else
            {
                cartItem.Quantity += 1;
                ca.HttpContext.Session.SetInt32("q", cartItem.Quantity);
            }
            HttpContext.Session.SetJson("Cart", cart);
            //  TempData["Success"] = "The product has been added!";

            //return Redirect(Request.Headers["Referer"].ToString());
            return RedirectToAction("Index", "Cart");
        }
        public async Task<IActionResult> Decrease(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            CartItem cartItem = cart.Where(c => c.ProductsId == id).FirstOrDefault();

            if (cartItem.Quantity > 1)
            {
                --cartItem.Quantity;
            }
            else
            {
                cart.RemoveAll(p => p.ProductsId == id);
            }

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }
            //   TempData["Success"] = "The product has been removed!";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Remove(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            cart.RemoveAll(p => p.ProductsId == id);

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            // TempData["Success"] = "The product has been removed!";

            return RedirectToAction("Index");
        }
        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("Index");
        }
    }
}
