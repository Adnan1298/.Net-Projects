using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Data;

namespace ShoppingCart.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PaymentController(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartViewModel cartVM = new CartViewModel
            {
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.Quantity * x.Price)
            };
            return View(cartVM);

        }
        public IActionResult Payment(Orders ord)
        {
            //return Ok(HttpContext.Session.GetString("id"));
            string firstName = HttpContext.Session.GetString("f");
            string email = HttpContext.Session.GetString("u");
            string? idies = HttpContext.Session.GetString("ids");
            string? userId = HttpContext.Session.GetString("id");
            int? pId = HttpContext.Session.GetInt32("pi");
            int? qty = HttpContext.Session.GetInt32("q");
            List<CartItem> carts = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            int totalAmount = (int)carts.Sum(x => x.Quantity * x.Price);
            ord.UsersId = userId;
            ord.user_product_id = idies;
            ord.TotalAmount = totalAmount.ToString();
            ord.Cardholdername = ord.Cardholdername;
            ord.CreditCard = ord.CreditCard;
            ord.Address = ord.Address ?? "";
            ord.ProductId = (int)pId;
            ord.Quantity = ord.Quantity;
            ord.Email = email;
            ord.Quantity = (int)qty;
            _dbContext.Orders.Add(ord);
            _dbContext.SaveChanges();

            // Clear the cart after successful payment
            HttpContext.Session.Remove("Cart");

            // Redirect to a success page or perform any other desired action
            return RedirectToAction("PaymentSuccess");
        }
        public IActionResult PaymentSuccess()
        {
            return View();
        }
    }
}
