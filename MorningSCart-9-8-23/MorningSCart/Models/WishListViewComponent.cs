using Microsoft.AspNetCore.Mvc;

namespace MorningSCart.Models
{
    public class WishListViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Wcart");
            WishListViewModel wishlistVM;

            if (cart == null || cart.Count == 0)
            {
                wishlistVM = null;
            }
            else
            {
                wishlistVM = new()
                {

                    NumberOfItems = cart.Sum(x => x.Quantity),
                    TotalAmount = cart.Sum(x => x.Quantity * x.Price)

                };
            }

            return View(wishlistVM);
        }
    }
}
