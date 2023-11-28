using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MorningSCart.Models;

namespace MorningSCart.Controllers
{
    public class PaymentController : Controller
    {
        private readonly DbScart _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PaymentController(DbScart dbContext, IHttpContextAccessor httpContextAccessor)
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
            //if (!ModelState.IsValid)
            //{
            //    // Get the cart items and calculate the grand total
            //    List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            //    CartViewModel cartVM = new CartViewModel
            //    {
            //        CartItems = cart,
            //        GrandTotal = cart.Sum(x => x.Quantity * x.Price)
            //    };

            //    // Return to the index view with validation errors and the correct model type
            //    return View("Index", cartVM);
            //}

            // Get the session values
            string firstName = HttpContext.Session.GetString("f");
         //   string lastName = HttpContext.Session.GetString("l");
            string email = HttpContext.Session.GetString("u");
			int? userId = HttpContext.Session.GetInt32("ui");
			int? pId = HttpContext.Session.GetInt32("pi");
            int? qty = HttpContext.Session.GetInt32("q");
            List<CartItem> carts = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
		//	ViewData["CategoryId"] = new SelectList(carts, "Plant_Id");
			// Calculate the total amount
			int totalAmount = (int)carts.Sum(x => x.Quantity * x.Price);
            
            //var checkoutEntity = new Orders
            //{
            //  UserName = $"{firstName} {lastName}",
            // Email = email,
           ord.UsersId = (int)userId;
            ord.TotalAmount = totalAmount.ToString();
            ord.Cardholdername = ord.Cardholdername;
            ord.CreditCard = ord.CreditCard;
            // CVV = model.CVV,
            ord.Address = ord.Address ?? "";
            ord.PlantsId = (int)pId;
            ord.Quantity = ord.Quantity;
            ord.Email = email;
            ord.Quantity = (int)qty;
            //};

            // Assuming you have a DbSet named "Payments" in your DbContext
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
