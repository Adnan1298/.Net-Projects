using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MorningSCart.Models
{
    public class CatViewComponent:ViewComponent
    {
       public  DbScart db;
        public CatViewComponent(DbScart dbc)
        {
            db=dbc;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
           return View(await db.Categories.ToListAsync());
        }
    }
}
