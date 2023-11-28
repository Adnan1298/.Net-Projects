using Microsoft.AspNetCore.Mvc;
using MorningSCart.Models;

namespace MorningSCart.Controllers
{
    public class WishListController : Controller
    {
        DbScart sc;
        IHttpContextAccessor ca;
        public WishListController(DbScart dbc, IHttpContextAccessor ca)
        {
            sc = dbc;
            this.ca = ca;
        }
        public IActionResult Index()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Wcart") ?? new List<CartItem>();

            CartViewModel cartVM = new()
            {
                //  pid=cart.(a=>a.ProductId),
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.Quantity * x.Price)


            };
            return View(cartVM);
        }
        public async Task<IActionResult> AddWS(int id)
        {
            ca.HttpContext.Session.SetInt32("pi", id);
            Plants product = await sc.Products.FindAsync(id);

            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Wcart") ?? new List<CartItem>();

            CartItem cartItem = cart.Where(c => c.ProductId == id).FirstOrDefault();

            if (cartItem == null)
            {
                cart.Add(new CartItem(product));
            }
            else
            {
                cartItem.Quantity += 1;
            }
            HttpContext.Session.SetJson("Wcart", cart);

            //  TempData["Success"] = "The product has been added!";

            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Wcart");
            return RedirectToAction("Index");
        }
    }
}
