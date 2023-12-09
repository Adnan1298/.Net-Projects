using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShoppingCart.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Collections.Generic;
using System.Security.AccessControl;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Data;

namespace PlantNest.Controllers
{
    public class UserController : Controller
    {
        ApplicationDbContext db;
        IHttpContextAccessor ca;
        public UserController(ApplicationDbContext dbc, IHttpContextAccessor ca)
        {
            db = dbc;
            this.ca = ca;
        }

        //public IActionResult Index()
        //{
        //    var query = from order in db.Orders
        //                join login in db.Users on order.UsersId equals login.Id
        //                join product in db.Products on order.ProductId equals product.ProductId
        //                select new OrderViewModel
        //                {
        //                    OrderId = order.Order_Id,
        //                    Username = login.UserName,
        //                    Email = login.Email,
        //                    //Address = login.HAddress,
        //                    OrderDate = order.DateTime,
        //                    CreditCardNo = order.CreditCard,
        //                    ProductName = product.Name,
        //                    ProductPrice = Convert.ToDecimal(product.Price),
        //                    Qty = order.Quantity,
        //                    TotalAmount = order.TotalAmount
        //                };
        //    var orders = query.ToList();
        //    return View(orders);
        //}
        public IActionResult ShowOrder()
        {
            string id = HttpContext.Session.GetString("id");
            //string email = "iqbaljawaid@gmail.com";
            var query = from order in db.Orders
                        join login in db.Users on order.UsersId equals login.Id
                        join product in db.Products on order.ProductId equals product.ProductId
                        where login.Id == id
                        select new OrderViewModel
                        {
                            OrderId = order.Order_Id,
                            Username = login.UserName,
                            Email = login.Email,
                            Address = order.Address,
                            OrderDate = order.DateTime,
                            CreditCardNo = order.CreditCard,
                            ProductName = product.Name,
                            ProductPrice = Convert.ToDecimal(product.Price),
                            Qty = order.Quantity,
                            Cardholdername=order.Cardholdername,
                            TotalAmount = order.TotalAmount
                        };
            var orders = query.ToList();
            return View(orders);
        }
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || db.Orders == null)
        //    {
        //        return NotFound();
        //    }

        //    var o = await db.Orders
        //        .FirstOrDefaultAsync(m => m.Order_Id == id);
        //    if (o == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(o);
        //}

        ////// POST: Cat/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (db.Orders == null)
        //    {
        //        return Problem("Entity set 'DbScart.Categories'  is null.");
        //    }
        //    var o = await db.Orders.FindAsync(id);
        //    if (o != null)
        //    {
        //        db.Orders.Remove(o);
        //    }

        //    await db.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

    }
}
