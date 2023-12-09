using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Data;

namespace ShoppingCart.Models
{
    public class CategoriesViewComponent : ViewComponent
    {
        public ApplicationDbContext db;
        public CategoriesViewComponent(ApplicationDbContext dbc)
        {
            db = dbc;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await db.Category.ToListAsync());
        }
    }
}
