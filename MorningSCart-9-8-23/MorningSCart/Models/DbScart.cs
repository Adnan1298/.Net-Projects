using Microsoft.EntityFrameworkCore;

namespace MorningSCart.Models
{
    public class DbScart:DbContext
    {
        public DbScart(DbContextOptions<DbScart> options) : base(options) { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Plants> Products { get; set; }
        public DbSet<Users>UserLogins { get; set; }
        public DbSet<Orders> Orders { get; set; }

    }
}
