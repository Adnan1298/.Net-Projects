using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MorningSCart.Models;
using System.Net;

namespace MorningSCart.Controllers
{
    public class LoginsController : Controller
    {
        DbScart db;
        IHttpContextAccessor ca;
        public LoginsController(DbScart dbc, IHttpContextAccessor cca)
        {
            db=dbc;
            ca=cca;
        }
            // GET: LoginsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: LoginsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LoginsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoginsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Users l)
        {
            db.UserLogins.Add(l);
            db.SaveChanges();
            ViewBag.m="Account Create Successfully";
            return View();
        }
        [HttpGet]
        public ActionResult UserLogin()
        {
			ViewData["uid"] = new SelectList(db.Categories, "User_Id", "Users");
			return View();
        }
		[HttpPost]
		public ActionResult UserLogin(Users lg)
		{
            //var x=from a in db.UserLogins
            //      where a.Email.Equals(lg.Email) && a.Password.Equals(lg.Password)
            //      select a;

            var y = db.UserLogins.Where(l => l.Email.Equals(lg.Email) && lg.Password.Equals(lg.Password)).SingleOrDefault();

            if(y!=null)
            {
               
                //var u = from a in db.UserLogins
                //        where a.UserName.Equals(lg.UserName)
                //        select new
                //        {
                //             a.User_Id
                //        };
             //   var e = db.UserLogins.Find(lg.Email);
              //  var ee=db.UserLogins.Where(a=>a.Email.Equals(lg.Email)).FirstOrDefault();
              //  var u = ee.User_Id;
              //  var u = new Users("uu", e.User_Id);
               
				//var l =new List<Users>
    //             {
    //                 new Users {"u",l.user}
    //             }
                ca.HttpContext.Session.SetString("u",y.Email);
               ca.HttpContext.Session.SetString("f", y.UserName);
                //string ui=u.ToString();
                ca.HttpContext.Session.SetInt32("ui",y.UsersId);


                return RedirectToAction("Index", "Home");
            }


            else
            {
                ViewBag.m="Wrong Email id or Password";
            }
			return View();
		}
		// GET: LoginsController/Edit/5
		public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LoginsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoginsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
